using System.Linq.Expressions;
using EnterpriseMediator.Core.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseMediator.Core.SharedKernel.Implementations.Data;

/// <summary>
/// A generic repository implementation using Entity Framework Core.
/// Implements both write and read interfaces for standard data access patterns.
/// </summary>
/// <typeparam name="T">The entity type this repository works with.</typeparam>
public class EfRepository<T> : IRepository<T>, IReadRepository<T> where T : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public EfRepository(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<T>();
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        // Using FindAsync is efficient as it checks the local cache first.
        return await _dbSet.FindAsync(new[] { id }, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(spec);
        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(spec);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<T?> SingleOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(spec);
        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(spec);
        return await query.CountAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(spec);
        return await query.AnyAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        
        // Convert to list to prevent multiple enumerations if it's a lazy enumerable
        var entityList = entities.ToList();
        if (entityList.Count == 0) return entityList;

        await _dbSet.AddRangeAsync(entityList, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entityList;
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        // In EF Core, Update marks the entity as Modified. 
        // If it's already tracked, it updates properties. If not, it attaches and sets state to Modified.
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));

        _dbSet.UpdateRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));

        _dbSet.RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Applies the specification to the default queryable set.
    /// Uses the SpecificationEvaluator logic defined in Level 1.
    /// </summary>
    /// <param name="spec">The specification describing filtering, inclusion, and ordering.</param>
    /// <returns>An IQueryable with the specification applied.</returns>
    protected virtual IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), spec);
    }
}