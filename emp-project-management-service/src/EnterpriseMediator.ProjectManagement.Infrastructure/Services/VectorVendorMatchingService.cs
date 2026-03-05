using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate;
using EnterpriseMediator.ProjectManagement.Domain.Services;
using EnterpriseMediator.ProjectManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pgvector;

namespace EnterpriseMediator.ProjectManagement.Infrastructure.Services
{
    /// <summary>
    /// Implementation of the vendor matching service using PostgreSQL pgvector.
    /// Performs semantic similarity search between Project Brief embeddings and Vendor Profile embeddings.
    /// </summary>
    public class VectorVendorMatchingService : IVendorMatchingService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly ILogger<VectorVendorMatchingService> _logger;

        public VectorVendorMatchingService(
            ProjectDbContext dbContext,
            ILogger<VectorVendorMatchingService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Finds vendors whose skills/profiles semantically match the provided project brief.
        /// Uses the Cosine Distance (<=>) operator via pgvector.
        /// </summary>
        /// <param name="brief">The project brief containing the source embedding.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <returns>A list of vendor matches with similarity scores.</returns>
        public async Task<IEnumerable<VendorMatch>> FindMatchingVendorsAsync(ProjectBrief brief, int limit)
        {
            if (brief == null) throw new ArgumentNullException(nameof(brief));
            if (brief.Embedding == null || brief.Embedding.Length == 0)
            {
                _logger.LogWarning("ProjectBrief {BriefId} has no embedding vector. Cannot perform matching.", brief.Id);
                return Enumerable.Empty<VendorMatch>();
            }

            if (limit <= 0) limit = 10;

            try
            {
                _logger.LogInformation("Executing vector search for ProjectBrief {BriefId} with limit {Limit}", brief.Id, limit);

                // Convert the float[] from the domain object to the pgvector Vector type
                var queryVector = new Vector(brief.Embedding);

                // We are querying a read-model table "VendorProfiles" that contains synchronized vendor embeddings.
                // This assumes the Project Management Service maintains a local projection of Vendor data
                // suitable for vector search (as per microservice autonomy principles).
                // If "VendorProfiles" is not mapped as a DbSet, we use FromSqlInterpolated.
                
                // Note: The cosine distance operator is <=> in pgvector. 
                // Similarity = 1 - Distance.
                
                var matches = await _dbContext.Database
                    .SqlQuery<VendorMatchResultInternal>($@"
                        SELECT 
                            ""Id"" as ""VendorId"",
                            ""CompanyName"",
                            1 - (""Embedding"" <=> {queryVector}) as ""SimilarityScore""
                        FROM ""VendorEmbeddings""
                        WHERE ""IsActive"" = true
                        ORDER BY ""Embedding"" <=> {queryVector}
                        LIMIT {limit}")
                    .ToListAsync();

                return matches.Select(m => new VendorMatch(
                    m.VendorId,
                    m.CompanyName,
                    m.SimilarityScore
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing vector search for ProjectBrief {BriefId}", brief.Id);
                throw new InvalidOperationException("Failed to execute vendor matching search.", ex);
            }
        }

        // Internal DTO to map raw SQL results before projecting to Domain DTO
        private sealed class VendorMatchResultInternal
        {
            public Guid VendorId { get; set; }
            public string CompanyName { get; set; } = string.Empty;
            public double SimilarityScore { get; set; }
        }
    }
}