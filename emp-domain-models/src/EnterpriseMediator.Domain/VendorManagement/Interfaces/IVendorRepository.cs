using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Shared.ValueObjects;
using EnterpriseMediator.Domain.VendorManagement.Aggregates;

namespace EnterpriseMediator.Domain.VendorManagement.Interfaces;

/// <summary>
/// Defines the contract for persistence and retrieval operations related to the Vendor aggregate.
/// This interface includes standard CRUD operations as well as specialized methods for 
/// semantic search functionality required by the vendor matching engine.
/// </summary>
public interface IVendorRepository : IRepository<Vendor>
{
    /// <summary>
    /// Adds a new vendor to the repository.
    /// </summary>
    /// <param name="vendor">The vendor entity to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The added vendor entity.</returns>
    Task<Vendor> AddAsync(Vendor vendor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing vendor in the repository.
    /// </summary>
    /// <param name="vendor">The vendor entity to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Vendor vendor, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a vendor by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vendor.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The vendor entity if found; otherwise, null.</returns>
    Task<Vendor?> GetByIdAsync(VendorId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a vendor by their primary contact email address.
    /// Essential for duplicate checking during the onboarding process (US-020).
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The vendor entity if found; otherwise, null.</returns>
    Task<Vendor?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of vendors based on their vetting status (e.g., Active, Pending Vetting).
    /// Used for administrative vendor management lists (US-021, US-023).
    /// </summary>
    /// <param name="status">The vendor status to filter by. Can be null to retrieve all.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of vendors matching the status filter.</returns>
    Task<IEnumerable<Vendor>> ListByStatusAsync(VendorStatus? status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs a semantic search to find vendors whose skill profiles match the provided embedding vector.
    /// This method supports REQ-FUNC-014 (AI-driven vendor matching).
    /// The implementation is expected to use vector similarity search (e.g., Cosine Similarity via pgvector).
    /// </summary>
    /// <param name="targetVector">The vector embedding representing the project requirements (from Project Brief).</param>
    /// <param name="limit">The maximum number of results to return (default 10).</param>
    /// <param name="minSimilarityThreshold">The minimum similarity score (0.0 to 1.0) required for a vendor to be included (default 0.7).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of vendors ranked by similarity to the target vector.</returns>
    Task<IEnumerable<Vendor>> GetByVectorSimilarityAsync(
        EmbeddingVector targetVector, 
        int limit = 10, 
        double minSimilarityThreshold = 0.7, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of vendors by a specific skill tag (exact match).
    /// Used for manual search and filtering by administrators (US-022).
    /// </summary>
    /// <param name="skill">The skill name/tag to search for.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of vendors possessing the specified skill.</returns>
    Task<IEnumerable<Vendor>> ListBySkillAsync(string skill, CancellationToken cancellationToken = default);
}