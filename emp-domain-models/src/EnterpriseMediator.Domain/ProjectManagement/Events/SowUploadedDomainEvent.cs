using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.UserManagement.Aggregates;

namespace EnterpriseMediator.Domain.ProjectManagement.Events;

/// <summary>
/// Domain event raised when a Statement of Work document is successfully uploaded.
/// This event typically triggers the AI ingestion workflow.
/// </summary>
/// <param name="ProjectId">The project associated with the SOW.</param>
/// <param name="SowId">The unique ID of the SOW document entity.</param>
/// <param name="StorageKey">The storage key (path) where the file is located.</param>
/// <param name="UploadedBy">The user who uploaded the document.</param>
/// <param name="OccurredOn">Timestamp of the event.</param>
public record SowUploadedDomainEvent(
    ProjectId ProjectId,
    Guid SowId,
    string StorageKey,
    UserId UploadedBy,
    DateTime OccurredOn
) : IDomainEvent;