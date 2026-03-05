using System;
using System.Collections.Generic;
using MediatR;

namespace EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.UpdateProjectBrief
{
    /// <summary>
    /// Command to update the AI-extracted Project Brief details.
    /// Allows System Administrators to manually correct or refine SOW data before finalizing the brief.
    /// </summary>
    /// <param name="ProjectId">The unique identifier of the project context.</param>
    /// <param name="ScopeSummary">The high-level summary of the project scope.</param>
    /// <param name="Deliverables">A list of specific deliverables expected from the vendor.</param>
    /// <param name="RequiredSkills">A list of technical or professional skills required for the project.</param>
    /// <param name="Timeline">The expected timeline or duration of the project.</param>
    /// <param name="Technologies">A list of specific technologies or stacks involved.</param>
    public record UpdateProjectBriefCommand(
        Guid ProjectId,
        string ScopeSummary,
        List<string> Deliverables,
        List<string> RequiredSkills,
        string Timeline,
        List<string> Technologies
    ) : IRequest;
}