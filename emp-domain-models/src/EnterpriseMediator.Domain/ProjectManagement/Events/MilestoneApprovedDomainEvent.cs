using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.ClientManagement.Aggregates;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.ProjectManagement.Events;

/// <summary>
/// Domain event raised when a client approves a project milestone.
/// This event typically triggers financial payouts or invoicing workflows.
/// </summary>
/// <param name="ProjectId">The unique identifier of the project.</param>
/// <param name="MilestoneId">The unique identifier of the approved milestone.</param>
/// <param name="ApprovedByContactId">The ID of the Client Contact who performed the approval.</param>
/// <param name="PayoutAmount">The amount to be released/paid for this milestone.</param>
/// <param name="OccurredOn">Timestamp of the event.</param>
public record MilestoneApprovedDomainEvent(
    ProjectId ProjectId,
    Guid MilestoneId,
    Guid ApprovedByContactId,
    Money PayoutAmount,
    DateTime OccurredOn
) : IDomainEvent;