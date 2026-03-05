using System;
using MediatR;

namespace EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.AwardProject
{
    /// <summary>
    /// Command to award a specific project to a winning proposal.
    /// This initiates the transition of the Project to the 'Awarded' state and triggers downstream financial workflows.
    /// </summary>
    /// <param name="ProjectId">The unique identifier of the project to be awarded.</param>
    /// <param name="ProposalId">The unique identifier of the winning proposal.</param>
    public record AwardProjectCommand(Guid ProjectId, Guid ProposalId) : IRequest;
}