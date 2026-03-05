using System;

namespace EnterpriseMediator.Domain.ClientManagement.Aggregates
{
    /// <summary>
    /// Strongly typed identifier for a Client.
    /// </summary>
    /// <param name="Value">The underlying GUID value.</param>
    public readonly record struct ClientId(Guid Value) : IComparable<ClientId>
    {
        /// <summary>
        /// Creates a new unique ClientId.
        /// </summary>
        public static ClientId New() => new(Guid.NewGuid());

        /// <summary>
        /// Represents an empty ClientId.
        /// </summary>
        public static ClientId Empty => new(Guid.Empty);

        public int CompareTo(ClientId other) => Value.CompareTo(other.Value);

        public override string ToString() => Value.ToString();
    }
}