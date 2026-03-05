using System.Linq;
using Microsoft.EntityFrameworkCore;
using EnterpriseMediator.Core.SharedKernel.Abstractions;

namespace EnterpriseMediator.Core.SharedKernel.Implementations.Data
{
    /// <summary>
    /// Evaluates domain specifications against Entity Framework Core IQueryable.
    /// Translates abstract specification rules into EF Core specific query operations.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity being queried.</typeparam>
    public class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            // Modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply ordering if specified
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                (current, include) => current.Include(include));

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }

            // Apply tracking settings
            if (!specification.IsTracking)
            {
                query = query.AsNoTracking();
            }
            
            // Apply split query settings
            if (specification.AsSplitQuery)
            {
                query = query.AsSplitQuery();
            }

            return query;
        }
    }
}