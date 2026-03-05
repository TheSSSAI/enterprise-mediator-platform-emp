using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Emp.ApiGateway.Application.Interfaces.Infrastructure
{
    /// <summary>
    /// Contract for communicating with the downstream Project Microservice.
    /// Handles project lifecycle data and document management.
    /// </summary>
    public interface IProjectServiceClient
    {
        /// <summary>
        /// Retrieves detailed information about a specific project from the Project Service.
        /// </summary>
        /// <param name="projectId">The unique identifier of the project.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Internal project details DTO if found, otherwise null.</returns>
        Task<InternalProjectDto?> GetProjectDetailsAsync(Guid projectId, CancellationToken ct);

        /// <summary>
        /// Sends a request to the Project Service to create a new project entity.
        /// </summary>
        /// <param name="dto">The project creation data.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The unique identifier of the created project.</returns>
        Task<Guid> CreateProjectAsync(CreateProjectDto dto, CancellationToken ct);

        /// <summary>
        /// Uploads a Statement of Work (SOW) document stream to the Project Service.
        /// </summary>
        /// <param name="projectId">The project identifier to associate the SOW with.</param>
        /// <param name="fileStream">The file content stream.</param>
        /// <param name="fileName">The original name of the file.</param>
        /// <param name="contentType">The MIME type of the file.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UploadSowAsync(Guid projectId, Stream fileStream, string fileName, string contentType, CancellationToken ct);
    }

    /// <summary>
    /// Data Transfer Object representing the internal view of a project returned by the microservice.
    /// </summary>
    public record InternalProjectDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public Guid ClientId { get; init; }
        public string Status { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for creating a new project.
    /// </summary>
    public record CreateProjectDto(string Name, string Description, Guid ClientId, DateTime? StartDate, DateTime? EndDate);
}