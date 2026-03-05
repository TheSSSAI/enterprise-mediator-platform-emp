using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.UserManagement.Application.Interfaces; // Assuming IDomainEventDispatcher interface location
using EnterpriseMediator.UserManagement.Domain.Common; // Assuming BaseEntity is here
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.UserManagement.Infrastructure.Services
{
    /// <summary>
    /// Service responsible for dispatching domain events recorded on entities.
    /// This is typically called by the DbContext during or after SaveChanges.
    /// </summary>
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IPublisher _mediator;
        private readonly ILogger<DomainEventDispatcher> _logger;

        public DomainEventDispatcher(IPublisher mediator, ILogger<DomainEventDispatcher> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Dispatches all domain events found in the provided entities.
        /// Clears the events from the entities immediately before publishing to prevent duplicate dispatching.
        /// </summary>
        /// <param name="entitiesWithEvents">The collection of entities that may have domain events.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents, CancellationToken cancellationToken = default)
        {
            if (entitiesWithEvents == null) return;

            var entities = entitiesWithEvents.Where(e => e.DomainEvents.Any()).ToList();

            if (!entities.Any()) return;

            // Collect all events from all entities
            var domainEvents = entities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            // Clear events from entities immediately to ensure they aren't fired again if SaveChanges is called subsequently
            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }

            _logger.LogDebug("Dispatching {Count} domain events...", domainEvents.Count);

            foreach (var domainEvent in domainEvents)
            {
                try
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                    _logger.LogDebug("Dispatched domain event: {EventName}", domainEvent.GetType().Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error dispatching domain event {EventName}", domainEvent.GetType().Name);
                    // We typically do not catch exceptions here to allow the transaction to fail/rollback 
                    // if a critical side-effect (handled synchronously) fails.
                    throw;
                }
            }
        }
    }
}