using System;

namespace EnterpriseMediator.Domain.Common
{
    /// <summary>
    /// Marker interface for domain events that occur within the domain layer.
    /// Represents a significant change in state or a business occurrence.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// The unique identifier of the event.
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// The timestamp when the event occurred (UTC).
        /// </summary>
        DateTime OccurredOn { get; }
    }
}