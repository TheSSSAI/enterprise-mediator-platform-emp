using System;

namespace EnterpriseMediator.Domain.UserManagement.Aggregates
{
    /// <summary>
    /// Strongly typed identifier for a Role.
    /// </summary>
    /// <param name="Value">The underlying GUID value.</param>
    public readonly record struct RoleId(Guid Value) : IComparable<RoleId>
    {
        /// <summary>
        /// Creates a new unique RoleId.
        /// </summary>
        public static RoleId New() => new(Guid.NewGuid());

        /// <summary>
        /// Represents an empty RoleId.
        /// </summary>
        public static RoleId Empty => new(Guid.Empty);

        public int CompareTo(RoleId other) => Value.CompareTo(other.Value);

        public override string ToString() => Value.ToString();
    }
}