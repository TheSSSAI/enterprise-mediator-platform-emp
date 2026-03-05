using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.UserManagement.Aggregates;

namespace EnterpriseMediator.Domain.Audit.Aggregates;

/// <summary>
/// Represents an immutable audit record for compliance and security monitoring (REQ-FUN-005).
/// </summary>
public class AuditLog : Entity<Guid>
{
    public UserId ActorUserId { get; private set; }
    public string IpAddress { get; private set; }
    public string ActionType { get; private set; }
    public string EntityName { get; private set; }
    public string EntityId { get; private set; }
    public string Changes { get; private set; } // JSON representation of before/after
    public DateTimeOffset CreatedAt { get; private set; }

    // EF Core
    protected AuditLog() { }

    private AuditLog(UserId actorUserId, string ipAddress, string actionType, string entityName, string entityId, string changes)
    {
        if (string.IsNullOrWhiteSpace(actionType))
            throw new BusinessRuleValidationException("Audit action type is required.");

        if (string.IsNullOrWhiteSpace(entityName))
            throw new BusinessRuleValidationException("Audit entity name is required.");

        Id = Guid.NewGuid();
        ActorUserId = actorUserId;
        IpAddress = ipAddress;
        ActionType = actionType;
        EntityName = entityName;
        EntityId = entityId;
        Changes = changes;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Creates a new audit log entry.
    /// </summary>
    /// <param name="actorUserId">The user performing the action.</param>
    /// <param name="ipAddress">The IP address of the request.</param>
    /// <param name="actionType">The type of action (e.g., "Create", "Update", "Approve").</param>
    /// <param name="entityName">The name of the entity being affected (e.g., "Project", "Vendor").</param>
    /// <param name="entityId">The ID of the entity.</param>
    /// <param name="changes">A JSON string or description of the changes (Before/After).</param>
    /// <returns>A new AuditLog instance.</returns>
    public static AuditLog Create(
        UserId actorUserId,
        string ipAddress,
        string actionType,
        string entityName,
        string entityId,
        string changes)
    {
        return new AuditLog(actorUserId, ipAddress, actionType, entityName, entityId, changes);
    }
}