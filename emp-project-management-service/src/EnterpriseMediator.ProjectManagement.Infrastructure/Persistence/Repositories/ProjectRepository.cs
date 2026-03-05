using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate;
using EnterpriseMediator.ProjectManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.ProjectManagement.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// EF Core implementation of the Project Aggregate repository.
    /// Manages persistence operations for Projects and their related entities (Proposals, SOWs).
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectDbContext _context;
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(ProjectDbContext context, ILogger<ProjectRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds a new project to the database context.
        /// </summary>
        public async Task AddAsync(Project project, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(project);

            _logger.LogDebug("Adding new project with ID {ProjectId}", project.Id);
            await _context.Projects.AddAsync(project, cancellationToken);
        }

        /// <summary>
        /// Retrieves a project by its unique identifier.
        /// Returns null if not found.
        /// </summary>
        public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetByIdAsync called with empty Guid");
                return null;
            }

            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a project by ID including its Proposals collection eagerly loaded.
        /// Critical for operations like Awarding a project where proposal state must be checked/updated.
        /// </summary>
        public async Task<Project?> GetByIdWithProposalsAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("GetByIdWithProposalsAsync called with empty Guid");
                return null;
            }

            _logger.LogDebug("Retrieving project {ProjectId} with proposals", id);

            return await _context.Projects
                .Include(p => p.Proposals)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a project with its SOW details eagerly loaded.
        /// Useful for SOW review and brief updates.
        /// </summary>
        public async Task<Project?> GetByIdWithSowAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty) return null;

            // Note: Since SowDetails is configured as a Value Object mapped to JSONB or 
            // an Owned Entity in the same table, it is typically loaded by default.
            // However, if SOW Document is a separate entity, we include it here.
            // Based on SDS, SowDocument is a relation.
            
            return await _context.Projects
                // Assuming SowDocument is a navigation property if it exists as a separate entity
                // If SowDetails is just a property, no Include is needed, but we keep this method
                // signature valid for the pattern.
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing project in the database context.
        /// Note: In EF Core, if the entity is tracked, explicit Update is not strictly required 
        /// but is good practice for disconnected scenarios or explicit intent.
        /// </summary>
        public void Update(Project project)
        {
            ArgumentNullException.ThrowIfNull(project);

            _logger.LogDebug("Marking project {ProjectId} as updated", project.Id);
            _context.Projects.Update(project);
        }

        /// <summary>
        /// Checks if a project exists with the given ID.
        /// </summary>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .AnyAsync(p => p.Id == id, cancellationToken);
        }
    }
}