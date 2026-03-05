using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.ProjectManagement.Enums;

namespace EnterpriseMediator.Domain.ProjectManagement.Events;

/// <summary>
/// Domain event raised when a project's status transitions from one state to another.
/// </summary>
/// <param name="ProjectId">The unique identifier of the project.</param>
/// <param name="OldStatus">The previous status of the project.</param>
/// <param name="NewStatus">The new status of the project.</param>
/// <param name="ChangedBy">The ID of the user who initiated the change (if applicable, otherwise System).</param>
/// <param name="OccurredOn">Timestamp of the event.</param>
public record ProjectStatusChangedDomainEvent(
    ProjectId ProjectId,
    ProjectStatus OldStatus,
    ProjectStatus NewStatus,
    Guid? ChangedBy,
    DateTime OccurredOn
) : IDomainEvent;