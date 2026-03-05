using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMediator.Core.SharedKernel.Abstractions;

/// <summary>
/// Defines the contract for a read-only repository.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IReadRepository<T> where T : class
{
    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of all entities.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A list of all entities.</returns>
    Task<List<T>> ListAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of entities matching the specified specification.
    /// </summary>
    /// <param name="spec">The specification filtering the entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A list of matching entities.</returns>
    Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the number of entities matching the specified specification.
    /// </summary>
    /// <param name="spec">The specification filtering the entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The count of matching entities.</returns>
    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if any entity matches the specified specification.
    /// </summary>
    /// <param name="spec">The specification filtering the entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>True if at least one entity matches; otherwise, false.</returns>
    Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a single entity matching the specified specification.
    /// </summary>
    /// <param name="spec">The specification filtering the entities.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The matching entity if found; otherwise, null.</returns>
    Task<T?> GetBySpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
}