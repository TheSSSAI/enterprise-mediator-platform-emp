using System;

namespace EnterpriseMediator.Domain.UserManagement.Aggregates
{
    /// <summary>
    /// Strongly typed identifier for a User.
    /// </summary>
    /// <param name="Value">The underlying GUID value.</param>
    public readonly record struct UserId(Guid Value) : IComparable<UserId>
    {
        /// <summary>
        /// Creates a new unique UserId.
        /// </summary>
        public static UserId New() => new(Guid.NewGuid());

        /// <summary>
        /// Represents an empty UserId.
        /// </summary>
        public static UserId Empty => new(Guid.Empty);

        public int CompareTo(UserId other) => Value.CompareTo(other.Value);

        public override string ToString() => Value.ToString();
    }
}