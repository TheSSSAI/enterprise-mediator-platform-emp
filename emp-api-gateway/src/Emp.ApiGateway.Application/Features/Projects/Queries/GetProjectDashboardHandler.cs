using AutoMapper;
using Emp.ApiGateway.Application.DTOs.Public;
using Emp.ApiGateway.Application.Interfaces.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Emp.ApiGateway.Application.Features.Projects.Queries
{
    /// <summary>
    /// Orchestrates the aggregation of data for the Project Dashboard.
    /// Fetches data in parallel from Project and Financial microservices.
    /// </summary>
    public class GetProjectDashboardHandler : IRequestHandler<GetProjectDashboardQuery, ProjectDashboardResponse>
    {
        private readonly IProjectServiceClient _projectService;
        private readonly IFinancialServiceClient _financialService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProjectDashboardHandler> _logger;

        public GetProjectDashboardHandler(
            IProjectServiceClient projectService,
            IFinancialServiceClient financialService,
            IMapper mapper,
            ILogger<GetProjectDashboardHandler> logger)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _financialService = financialService ?? throw new ArgumentNullException(nameof(financialService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProjectDashboardResponse> Handle(GetProjectDashboardQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting dashboard aggregation for ProjectId: {ProjectId}", request.ProjectId);

            try
            {
                // Execute service calls in parallel to minimize latency
                var projectTask = _projectService.GetProjectDetailsAsync(request.ProjectId, cancellationToken);
                var financialTask = _financialService.GetProjectFinancialSummaryAsync(request.ProjectId, cancellationToken);

                await Task.WhenAll(projectTask, financialTask);

                var projectDetails = await projectTask;
                var financialSummary = await financialTask;

                if (projectDetails == null)
                {
                    _logger.LogWarning("Project details not found for ProjectId: {ProjectId}", request.ProjectId);
                    // Depending on global exception handling strategy, we might throw a NotFoundException here
                    // For now, we assume the controller will handle a null response or we throw here.
                    throw new KeyNotFoundException($"Project with ID {request.ProjectId} not found.");
                }

                // Map to Public DTOs
                var publicProject = _mapper.Map<PublicProjectDto>(projectDetails);
                var publicFinancials = financialSummary != null 
                    ? _mapper.Map<PublicFinancialSummaryDto>(financialSummary)
                    : null;

                _logger.LogInformation("Successfully aggregated dashboard data for ProjectId: {ProjectId}", request.ProjectId);

                return new ProjectDashboardResponse
                {
                    Project = publicProject,
                    Financials = publicFinancials
                };
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                _logger.LogError(ex, "Failed to aggregate dashboard for ProjectId: {ProjectId}", request.ProjectId);
                throw;
            }
        }
    }
}