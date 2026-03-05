using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.UserManagement.Application.Interfaces;
using EnterpriseMediator.UserManagement.Application.Models; // Assuming AuditLogEntry DTO is here
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.UserManagement.Infrastructure.Services
{
    /// <summary>
    /// Infrastructure adapter implementation for the Audit Service.
    /// Decouples the application layer from the specific message broker technology (MassTransit).
    /// </summary>
    public class AuditServiceAdapter : IAuditServiceAdapter
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<AuditServiceAdapter> _logger;

        public AuditServiceAdapter(
            IPublishEndpoint publishEndpoint,
            ILogger<AuditServiceAdapter> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Asynchronously publishes an audit log entry to the message bus.
        /// This ensures the user management service does not block or fail if the audit service is temporarily unavailable,
        /// relying on the message broker's durability.
        /// </summary>
        /// <param name="entry">The audit log entry DTO.</param>
        /// <param name="ct">Cancellation token.</param>
        public async Task LogEventAsync(AuditLogEntry entry, CancellationToken ct = default)
        {
            if (entry == null)
            {
                _logger.LogWarning("Attempted to log a null audit entry. Operation skipped.");
                return;
            }

            try
            {
                // We publish the contract directly. 
                // In a real scenario, this might map to a specific 'AuditLogIntegrationEvent' defined in a shared contracts library.
                // For this implementation, we assume AuditLogEntry matches the message contract.
                
                await _publishEndpoint.Publish(entry, ct);

                _logger.LogDebug("Audit event {EventType} for Entity {EntityId} published to bus.", 
                    entry.EventType, 
                    entry.EntityId);
            }
            catch (Exception ex)
            {
                // We catch exceptions here to ensure that failure to audit does not necessarily crash the business transaction
                // in the calling scope, although strictly critical systems might want to throw.
                // Given the requirement for reliable auditing, in a production system with Outbox pattern,
                // the database transaction would handle the outbox message, making this safe.
                // Without Outbox, we log heavily here.
                
                _logger.LogError(ex, "Failed to publish audit event {EventType} for Entity {EntityId}.", 
                    entry.EventType, 
                    entry.EntityId);
                
                // Depending on strictness of audit requirements, we might want to rethrow.
                // For this implementation, we log error to ensure observability but protect the main thread.
                // throw; 
            }
        }
    }
}