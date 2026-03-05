using System;
using System.Collections.Generic;
using MediatR;

namespace EnterpriseMediator.ProjectManagement.Application.Features.Proposals.Queries.GetProjectProposals
{
    /// <summary>
    /// Query to retrieve all proposals submitted for a specific project.
    /// Used by the Proposal Management Dashboard.
    /// </summary>
    /// <param name="ProjectId">The unique identifier of the project to fetch proposals for.</param>
    public record GetProjectProposalsQuery(Guid ProjectId) : IRequest<IEnumerable<ProposalDto>>;

    /// <summary>
    /// Data Transfer Object representing a high-level view of a Proposal.
    /// Optimized for list/grid display in the UI.
    /// </summary>
    /// <param name="Id">The unique identifier of the proposal.</param>
    /// <param name="VendorId">The unique identifier of the vendor submitting the proposal.</param>
    /// <param name="Cost">The total proposed cost.</param>
    /// <param name="Timeline">The proposed timeline execution summary.</param>
    /// <param name="Status">The current status of the proposal (e.g., Submitted, Accepted, Rejected).</param>
    /// <param name="SubmittedAt">The timestamp when the proposal was submitted.</param>
    public record ProposalDto(
        Guid Id,
        Guid VendorId,
        decimal Cost,
        string Timeline,
        string Status,
        DateTime SubmittedAt
    );
}