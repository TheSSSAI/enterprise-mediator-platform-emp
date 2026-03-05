using System.Reflection;
using EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseMediator.ProjectManagement.Infrastructure.Persistence;

/// <summary>
/// The Entity Framework Core DbContext for the Project Management Bounded Context.
/// Handles persistence for Projects, Proposals, and related domain entities.
/// Implements the Unit of Work pattern and handles Domain Event dispatching.
/// </summary>
public class ProjectDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public ProjectDbContext(
        DbContextOptions<ProjectDbContext> options,
        IPublisher publisher) : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Proposal> Proposals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enable pgvector extension for Semantic Search requirements (REQ-FUNC-014)
        modelBuilder.HasPostgresExtension("vector");

        // Apply all configurations defined in the current assembly (e.g., ProjectConfiguration)
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Additional global conventions or filter configurations could go here
    }

    /// <summary>
    /// Overrides SaveChangesAsync to orchestrate Domain Event dispatching.
    /// This ensures that side effects triggered by domain logic are processed
    /// within the transaction lifecycle (or immediately before/after).
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Strategy: Dispatch Domain Events BEFORE committing to the database.
        // This allows event handlers to participate in the same transaction context 
        // if configured (e.g., creating other entities) or for the save to fail 
        // if a critical domain rule (enforced via event handler) fails.
        // For integration events (Outbox), the event handlers would write to the Outbox table here.
        
        await DispatchDomainEventsAsync();

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    private async Task DispatchDomainEventsAsync()
    {
        var domainEventEntities = ChangeTracker.Entries<AggregateRoot<Guid>>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents != null && po.DomainEvents.Any())
            .ToArray();

        if (!domainEventEntities.Any())
        {
            return;
        }

        var domainEvents = domainEventEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        // Clear domain events to prevent double-dispatching if SaveChanges is called again
        foreach (var entity in domainEventEntities)
        {
            entity.ClearDomainEvents();
        }

        // Publish events to in-process handlers (MediatR)
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}