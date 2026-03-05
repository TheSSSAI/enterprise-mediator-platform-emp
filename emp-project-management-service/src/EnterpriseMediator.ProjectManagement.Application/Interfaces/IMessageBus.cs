using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMediator.ProjectManagement.Application.Interfaces
{
    /// <summary>
    /// Abstraction for the message bus infrastructure to publish integration events.
    /// Decouples the Application layer from specific message broker implementations (e.g., MassTransit/RabbitMQ).
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Publishes an integration event to the message bus asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the message/event to publish. Must be a reference type.</typeparam>
        /// <param name="message">The event message payload.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous publish operation.</returns>
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}