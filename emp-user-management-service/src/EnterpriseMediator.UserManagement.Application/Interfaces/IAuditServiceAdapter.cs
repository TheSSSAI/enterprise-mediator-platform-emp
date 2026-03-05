using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMediator.UserManagement.Application.Interfaces;

/// <summary>
/// Adapter interface for the external Audit Service.
/// Decouples the Application layer from specific message bus implementations (MassTransit/Azure Service Bus).
/// </summary>
public interface IAuditServiceAdapter
{
    /// <summary>
    /// Publishes an audit log entry to the audit system asynchronously.
    /// </summary>
    /// <param name="entry">The audit log data entry.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LogEventAsync(AuditLogEntry entry, CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents the data structure for an audit log event.
/// </summary>
public record AuditLogEntry
{
    public Guid UserId { get; init; }
    public string Action { get; init; } = string.Empty;
    public string EntityType { get; init; } = string.Empty;
    public string EntityId { get; init; } = string.Empty;
    public string Details { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public string IpAddress { get; init; } = string.Empty;

    public AuditLogEntry(Guid userId, string action, string entityType, string entityId, string details, string ipAddress = "")
    {
        UserId = userId;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        Details = details;
        IpAddress = ipAddress;
    }
}