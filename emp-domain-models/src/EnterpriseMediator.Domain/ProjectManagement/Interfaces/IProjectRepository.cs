using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.Domain.ClientManagement.Aggregates;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.ProjectManagement.Enums;

namespace EnterpriseMediator.Domain.ProjectManagement.Interfaces;

/// <summary>
/// Defines the contract for persistence and retrieval operations related to the Project aggregate.
/// This interface abstracts the underlying data access mechanism, allowing the domain logic 
/// to remain agnostic of the persistence technology (e.g., EF Core, Dapper).
/// </summary>
public interface IProjectRepository : IRepository<Project>
{
    /// <summary>
    /// Adds a new project to the repository.
    /// </summary>
    /// <param name="project">The project entity to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The added project entity.</returns>
    Task<Project> AddAsync(Project project, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing project in the repository.
    /// </summary>
    /// <param name="project">The project entity to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Project project, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a project by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The project entity if found; otherwise, null.</returns>
    Task<Project?> GetByIdAsync(ProjectId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a project by its unique identifier, eagerly loading its associated Statement of Work (SOW).
    /// This is critical for the AI processing and SOW review workflows.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The project entity with SOW loaded if found; otherwise, null.</returns>
    Task<Project?> GetByIdWithSowAsync(ProjectId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a project by its unique identifier, eagerly loading its associated Project Brief.
    /// Required for the vendor matching and distribution workflows.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The project entity with Brief loaded if found; otherwise, null.</returns>
    Task<Project?> GetByIdWithBriefAsync(ProjectId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a project by its unique identifier, eagerly loading all associated Proposals.
    /// This is essential for the proposal comparison and project awarding workflows to ensure
    /// aggregate consistency when changing proposal statuses.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The project entity with Proposals loaded if found; otherwise, null.</returns>
    Task<Project?> GetByIdWithProposalsAsync(ProjectId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of projects associated with a specific client.
    /// Used for client dashboards and reporting.
    /// </summary>
    /// <param name="clientId">The unique identifier of the client.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of projects belonging to the client.</returns>
    Task<IEnumerable<Project>> ListByClientAsync(ClientId clientId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of projects filtered by their current status.
    /// Used for administrative pipeline reporting and operational oversight.
    /// </summary>
    /// <param name="status">The project status to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of projects matching the status.</returns>
    Task<IEnumerable<Project>> ListByStatusAsync(ProjectStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a project exists with the given name for a specific client to enforce uniqueness constraints if required by domain rules.
    /// </summary>
    /// <param name="name">The project name to check.</param>
    /// <param name="clientId">The client ID context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if a duplicate exists; otherwise, false.</returns>
    Task<bool> ExistsByNameAsync(string name, ClientId clientId, CancellationToken cancellationToken = default);
}