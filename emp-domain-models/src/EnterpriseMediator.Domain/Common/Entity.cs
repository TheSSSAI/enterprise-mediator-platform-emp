using System;
using System.Collections.Generic;

namespace EnterpriseMediator.Domain.Common
{
    /// <summary>
    /// Base class for entities in the domain layer.
    /// Entities are objects that have a distinct identity that runs through time and different representations.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// The unique identifier for this entity.
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Protected constructor for ORMs and derived classes.
        /// </summary>
        protected Entity() { }

        /// <summary>
        /// Initializes a new instance of the entity with a specific ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected Entity(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// Collection of domain events that occurred on this entity.
        /// These should be dispatched/published when the entity is persisted.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a domain event to the entity's event collection.
        /// </summary>
        /// <param name="domainEvent">The event to add.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a specific domain event.
        /// </summary>
        /// <param name="domainEvent">The event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events from the entity.
        /// Typically called after events have been dispatched.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((Entity<TId>)obj);
        }

        public bool Equals(Entity<TId>? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
        {
            return !(left == right);
        }
    }
}