using System;
using System.Linq.Expressions;

namespace EnterpriseMediator.Domain.Common
{
    /// <summary>
    /// Contract for the Specification pattern.
    /// Encapsulates domain rules for querying or validating entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity to which the specification applies.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// The expression that defines the specification criteria.
        /// Can be used with LINQ providers (e.g., EF Core).
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Checks if a specific entity satisfies the specification in memory.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <returns>True if satisfied; otherwise false.</returns>
        bool IsSatisfiedBy(T entity);
    }
}