using System;

namespace EnterpriseMediator.Domain.ProjectManagement.Aggregates
{
    /// <summary>
    /// Strongly typed identifier for a Project.
    /// Uses a record struct for immutability, value semantics, and performance efficiency.
    /// </summary>
    /// <param name="Value">The underlying GUID value.</param>
    public readonly record struct ProjectId(Guid Value) : IComparable<ProjectId>
    {
        /// <summary>
        /// Creates a new unique ProjectId.
        /// </summary>
        public static ProjectId New() => new(Guid.NewGuid());

        /// <summary>
        /// Represents an empty ProjectId.
        /// </summary>
        public static ProjectId Empty => new(Guid.Empty);

        public int CompareTo(ProjectId other) => Value.CompareTo(other.Value);

        public override string ToString() => Value.ToString();
    }
}