using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EnterpriseMediator.Core.SharedKernel.Abstractions;

/// <summary>
/// Defines a specification for querying entities, encapsulating filtering, including, sorting, and paging logic.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the filtering criteria expression.
    /// </summary>
    Expression<Func<T, bool>>? Criteria { get; }

    /// <summary>
    /// Gets the list of navigation properties to include (eager loading).
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Gets the list of navigation properties to include via string path.
    /// </summary>
    List<string> IncludeStrings { get; }

    /// <summary>
    /// Gets the sorting expression for ascending order.
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// Gets the sorting expression for descending order.
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// Gets the grouping expression.
    /// </summary>
    Expression<Func<T, object>>? GroupBy { get; }

    /// <summary>
    /// Gets the number of records to skip for paging.
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Gets the number of records to take for paging.
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Gets a value indicating whether paging is enabled.
    /// </summary>
    bool IsPagingEnabled { get; }

    /// <summary>
    /// Gets a value indicating whether caching should be enabled for this query.
    /// </summary>
    bool CacheEnabled { get; }

    /// <summary>
    /// Gets the cache key for this specification.
    /// </summary>
    string? CacheKey { get; }

    /// <summary>
    /// Gets a value indicating whether change tracking should be disabled (AsNoTracking).
    /// </summary>
    bool IsAsNoTracking { get; }
}