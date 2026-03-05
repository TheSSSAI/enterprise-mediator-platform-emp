using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.ClientManagement.Aggregates;

namespace EnterpriseMediator.Domain.ProjectManagement.Events;

/// <summary>
/// Domain event raised when a new project is created.
/// </summary>
/// <param name="ProjectId">The unique identifier of the created project.</param>
/// <param name="ClientId">The client associated with the project.</param>
/// <param name="ProjectName">The name of the project.</param>
/// <param name="CreatedBy">The ID of the user who created the project (System Admin).</param>
/// <param name="OccurredOn">Timestamp of the event.</param>
public record ProjectCreatedDomainEvent(
    ProjectId ProjectId,
    ClientId ClientId,
    string ProjectName,
    Guid CreatedBy,
    DateTime OccurredOn
) : IDomainEvent;