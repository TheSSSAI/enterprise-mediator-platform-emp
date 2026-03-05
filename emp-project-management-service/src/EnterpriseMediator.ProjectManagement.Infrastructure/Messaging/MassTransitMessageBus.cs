using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.ProjectManagement.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.ProjectManagement.Infrastructure.Messaging
{
    /// <summary>
    /// Infrastructure implementation of the Application layer's IMessageBus.
    /// Adapts MassTransit's IPublishEndpoint to the domain's messaging abstraction.
    /// Used for publishing integration events to the message broker (RabbitMQ/SQS).
    /// </summary>
    public class MassTransitMessageBus : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<MassTransitMessageBus> _logger;

        public MassTransitMessageBus(
            IPublishEndpoint publishEndpoint, 
            ILogger<MassTransitMessageBus> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Publishes an event message to the configured message broker.
        /// This creates a decoupling between the application logic and the specific broker implementation.
        /// </summary>
        /// <typeparam name="T">The type of the message event.</typeparam>
        /// <param name="message">The event message payload.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) 
            where T : class
        {
            ArgumentNullException.ThrowIfNull(message);

            var messageType = typeof(T).Name;

            try
            {
                _logger.LogInformation("Publishing integration event {MessageType} via MassTransit", messageType);
                
                await _publishEndpoint.Publish(message, cancellationToken);
                
                _logger.LogDebug("Successfully published integration event {MessageType}", messageType);
            }
            catch (Exception ex)
            {
                // We log the error but we generally rethrow it to ensure the transaction 
                // (if using Outbox) or the caller knows the publish failed.
                _logger.LogError(ex, "Failed to publish integration event {MessageType}", messageType);
                throw;
            }
        }
    }
}