using Emp.ApiGateway.Application.Interfaces.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Emp.ApiGateway.Infrastructure.Messaging
{
    /// <summary>
    /// Wrapper around MassTransit's IPublishEndpoint to abstract the messaging infrastructure.
    /// Implements IMessageBus defined in the Application layer.
    /// </summary>
    public class MassTransitPublisher : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<MassTransitPublisher> _logger;

        public MassTransitPublisher(IPublishEndpoint publishEndpoint, ILogger<MassTransitPublisher> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                _logger.LogInformation("Publishing message of type {MessageType}", typeof(T).Name);
                await _publishEndpoint.Publish(message, cancellationToken);
                _logger.LogInformation("Successfully published message {MessageType}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish message {MessageType}", typeof(T).Name);
                throw;
            }
        }
    }
}