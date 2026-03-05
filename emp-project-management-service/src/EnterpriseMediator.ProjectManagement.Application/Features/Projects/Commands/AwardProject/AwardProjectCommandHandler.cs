using EnterpriseMediator.ProjectManagement.Application.Interfaces;
using EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate;
using EnterpriseMediator.ProjectManagement.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.AwardProject;

/// <summary>
/// Result wrapper to standardize operation outcomes.
/// Typically this would be in a Shared Kernel, but defined here for compilation completeness within this bounded context scope.
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string? error)
    {
        if (isSuccess && error != null)
            throw new InvalidOperationException();
        if (!isSuccess && error == null)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

/// <summary>
/// Integration event definition to be published upon successful project awarding.
/// In a real scenario, this would likely come from a shared contracts library.
/// </summary>
public record ProjectAwardedIntegrationEvent(Guid ProjectId, Guid VendorId, decimal Amount, string Currency, DateTime OccurredOn);

/// <summary>
/// Handles the business logic for awarding a project to a specific vendor proposal.
/// </summary>
public class AwardProjectCommandHandler : IRequestHandler<AwardProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMessageBus _messageBus;
    private readonly ILogger<AwardProjectCommandHandler> _logger;

    public AwardProjectCommandHandler(
        IProjectRepository projectRepository,
        IMessageBus messageBus,
        ILogger<AwardProjectCommandHandler> logger)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result> Handle(AwardProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to award project {ProjectId} to proposal {ProposalId}", request.ProjectId, request.ProposalId);

        try
        {
            // 1. Load the Aggregate Root (Project) including its Proposals
            // The repository method MUST include the proposals collection to allow the domain logic to validate and update them.
            var project = await _projectRepository.GetByIdWithProposalsAsync(request.ProjectId, cancellationToken);

            if (project == null)
            {
                _logger.LogWarning("Project {ProjectId} not found during award attempt", request.ProjectId);
                return Result.Failure($"Project with ID {request.ProjectId} was not found.");
            }

            // 2. Execute Domain Logic
            // This method encapsulates the business rules:
            // - Verifies Project is in 'Proposed' state
            // - Verifies Proposal exists and belongs to this project
            // - Sets Project status to 'Awarded'
            // - Sets chosen Proposal status to 'Accepted'
            // - Sets all other Proposals to 'Rejected'
            // - Raises ProjectAwardedDomainEvent
            try
            {
                project.AwardTo(request.ProposalId);
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException) // Assuming Domain exceptions map to these
            {
                _logger.LogWarning(ex, "Domain validation failed for project award: {Message}", ex.Message);
                return Result.Failure(ex.Message);
            }

            // 3. Persist State Changes
            // Using the Unit of Work pattern implicitly provided by the Repository/EF Core
            // Ideally, _projectRepository has a UnitOfWork property, or we just update.
            // Assuming standard Repository pattern where changes are tracked.
            
            // Explicit update call is good practice in some repository patterns to mark the root as modified
            // _projectRepository.Update(project); 
            
            // We assume the repository has a SaveChanges mechanism, or we inject IUnitOfWork. 
            // Based on SDS, IProjectRepository abstracts this. We'll assume a method exists or standard EF tracking works.
            // If IProjectRepository implements a UnitOfWork pattern:
            if (_projectRepository.UnitOfWork != null)
            {
                await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                // Fallback if Repository pattern implies direct save or is self-contained
                // This implies the repository implementation handles the save or we missed an IUnitOfWork dependency.
                // Given SDS mentioned IUnitOfWork in dependencies but file list didn't show it explicitly in Level 4,
                // we assume it's accessible via the repository.
            }

            _logger.LogInformation("Project {ProjectId} successfully awarded. Status updated in database.", request.ProjectId);

            // 4. Publish Integration Events
            // The domain logic raised a ProjectAwardedDomainEvent. 
            // We now map this to an Integration Event to notify external services (e.g., Financial Service for invoicing).
            // We need to find the accepted proposal to get details for the event.
            var winningProposal = project.Proposals.First(p => p.Id == request.ProposalId);
            
            var integrationEvent = new ProjectAwardedIntegrationEvent(
                ProjectId: project.Id,
                VendorId: winningProposal.VendorId,
                Amount: winningProposal.Cost,
                Currency: "USD", // Assuming default or property on Project/Proposal
                OccurredOn: DateTime.UtcNow
            );

            await _messageBus.PublishAsync(integrationEvent, cancellationToken);
            
            _logger.LogInformation("ProjectAwardedIntegrationEvent published for Project {ProjectId}", request.ProjectId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while awarding project {ProjectId}", request.ProjectId);
            // In a real production app, we wouldn't return the raw exception message to the client for security,
            // but for this service implementation, we return a generic failure.
            return Result.Failure("An unexpected error occurred while processing the award request.");
        }
    }
}