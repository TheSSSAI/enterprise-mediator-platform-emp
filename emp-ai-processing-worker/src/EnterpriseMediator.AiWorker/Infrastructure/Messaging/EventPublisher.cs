using EnterpriseMediator.AiWorker.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.AiWorker.Infrastructure.Messaging
{
    /// <summary>
    /// Wrapper around MassTransit's IPublishEndpoint to abstract the messaging infrastructure.
    /// Enables publishing domain events to the message bus (RabbitMQ).
    /// </summary>
    public class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<EventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task PublishAsync<T>(T eventMessage, CancellationToken token = default) where T : class
        {
            if (eventMessage == null)
            {
                _logger.LogWarning("Attempted to publish a null event message.");
                return;
            }

            var eventType = typeof(T).Name;

            try
            {
                _logger.LogInformation("Publishing event {EventType} to message bus.", eventType);

                await _publishEndpoint.Publish(eventMessage, context =>
                {
                    // Add correlation/diagnostic headers if needed
                    context.Headers.Set("Source", "EnterpriseMediator.AiWorker");
                }, token);

                _logger.LogInformation("Successfully published event {EventType}.", eventType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish event {EventType}.", eventType);
                throw new InvalidOperationException($"Messaging infrastructure failed to publish event: {eventType}", ex);
            }
        }
    }
}