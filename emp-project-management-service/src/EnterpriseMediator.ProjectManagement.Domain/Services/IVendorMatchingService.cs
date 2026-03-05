using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate;

namespace EnterpriseMediator.ProjectManagement.Domain.Services
{
    /// <summary>
    /// Represents a matched vendor result from the semantic search domain service.
    /// </summary>
    /// <param name="VendorId">The unique identifier of the vendor.</param>
    /// <param name="SimilarityScore">The cosine similarity score (0.0 to 1.0) indicating relevance.</param>
    /// <param name="MatchedSkills">A list of skills from the SOW that matched this vendor's profile.</param>
    public record VendorMatch(Guid VendorId, double SimilarityScore, List<string> MatchedSkills);

    /// <summary>
    /// Defines the contract for the domain service responsible for semantic vendor matching.
    /// This service bridges the domain requirement for "intelligent matching" with the 
    /// underlying vector search infrastructure capability.
    /// </summary>
    public interface IVendorMatchingService
    {
        /// <summary>
        /// Performs a semantic search to identify and rank vendors whose capabilities match 
        /// the requirements defined in the Statement of Work (SOW) details.
        /// </summary>
        /// <remarks>
        /// This method is expected to utilize vector embeddings generated from the SOW's 
        /// extracted skills and scope summary to query against the vendor vector database.
        /// </remarks>
        /// <param name="sowDetails">The structured SOW details containing skills, scope, and embeddings.</param>
        /// <param name="limit">The maximum number of recommendations to return. Default is 10.</param>
        /// <param name="minSimilarityThreshold">The minimum similarity score required for a vendor to be included. Default is 0.7.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A collection of vendor matches ranked by similarity score descending.</returns>
        Task<IEnumerable<VendorMatch>> FindMatchingVendorsAsync(
            SowDetails sowDetails, 
            int limit = 10, 
            double minSimilarityThreshold = 0.7, 
            CancellationToken cancellationToken = default);
    }
}