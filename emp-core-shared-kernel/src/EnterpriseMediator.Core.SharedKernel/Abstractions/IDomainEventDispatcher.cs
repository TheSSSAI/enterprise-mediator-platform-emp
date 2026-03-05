using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMediator.Core.SharedKernel.Abstractions;

/// <summary>
/// Defines the contract for dispatching domain events.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches all domain events pending in the provided entities and clears them.
    /// </summary>
    /// <param name="entitiesWithEvents">The collection of entities that may have domain events.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DispatchAndClearEvents(IEnumerable<object> entitiesWithEvents, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dispatches a single domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to dispatch.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Dispatch(object domainEvent, CancellationToken cancellationToken = default);
}