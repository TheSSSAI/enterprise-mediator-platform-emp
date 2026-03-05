using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate;

namespace EnterpriseMediator.ProjectManagement.Domain.Interfaces
{
    /// <summary>
    /// Defines the persistence contract for the Project Aggregate Root.
    /// Follows the Repository pattern to abstract data access logic from the domain layer.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Retrieves a Project by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The Project aggregate root if found; otherwise, null.</returns>
        Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a Project by its unique identifier, explicitly including the Proposals collection.
        /// This is optimized for the 'Award Project' workflow where Proposal state validation is critical.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The Project aggregate root with Proposals populated if found; otherwise, null.</returns>
        Task<Project?> GetByIdWithProposalsAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new Project entity to the repository.
        /// </summary>
        /// <param name="project">The project to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task AddAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing Project entity in the repository.
        /// Implementation should handle concurrency conflicts and state tracking.
        /// </summary>
        /// <param name="project">The project to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a project exists with the given identifier.
        /// Useful for lightweight validation checks.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the project exists; otherwise, false.</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}