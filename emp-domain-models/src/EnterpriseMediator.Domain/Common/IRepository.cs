using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMediator.Domain.Common
{
    /// <summary>
    /// Generic repository interface for Aggregate Roots.
    /// Defines standard CRUD operations compliant with DDD principles.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root.</typeparam>
    /// <typeparam name="TId">The type of the aggregate root's identifier.</typeparam>
    public interface IRepository<T, TId> where T : class, IAggregateRoot
    {
        /// <summary>
        /// Retrieves an aggregate root by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the aggregate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The aggregate root if found, otherwise null.</returns>
        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all aggregate roots.
        /// Note: Use with caution on large datasets; consider specific query methods instead.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all aggregate roots.</returns>
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new aggregate root to the repository.
        /// </summary>
        /// <param name="aggregate">The aggregate to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The added aggregate root.</returns>
        Task<T> AddAsync(T aggregate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing aggregate root in the repository.
        /// </summary>
        /// <param name="aggregate">The aggregate to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(T aggregate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an aggregate root from the repository.
        /// </summary>
        /// <param name="aggregate">The aggregate to remove.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteAsync(T aggregate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Persists changes made to the repository.
        /// Commonly known as Unit of Work commit in some patterns, 
        /// but explicit saving is often required in repository implementations.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}