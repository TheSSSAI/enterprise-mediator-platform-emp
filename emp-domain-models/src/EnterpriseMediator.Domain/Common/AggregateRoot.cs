using System.Collections.Generic;
using System.Collections.Immutable;

namespace EnterpriseMediator.Domain.Common
{
    /// <summary>
    /// Marker interface to identify aggregate roots in the domain.
    /// </summary>
    public interface IAggregateRoot { }

    /// <summary>
    /// Base class for all aggregate roots.
    /// Aggregate roots are the entry points to the object graph and ensure consistency boundaries.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Gets the immutable collection of domain events occurred in this aggregate.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected AggregateRoot() { }

        protected AggregateRoot(TId id) : base(id) { }

        /// <summary>
        /// Adds a domain event to the collection of events to be dispatched.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null) return;
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a specific domain event from the collection.
        /// </summary>
        /// <param name="domainEvent">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events from the aggregate.
        /// Typically called after events have been dispatched to the event bus.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}