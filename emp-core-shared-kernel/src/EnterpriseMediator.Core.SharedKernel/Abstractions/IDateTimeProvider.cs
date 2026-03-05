using System;

namespace EnterpriseMediator.Core.SharedKernel.Abstractions;

/// <summary>
/// Abstraction for accessing the current date and time to facilitate testing and consistency.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current date and time in Coordinated Universal Time (UTC).
    /// </summary>
    DateTime UtcNow { get; }

    /// <summary>
    /// Gets the current date and time in the local time zone.
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// Gets the current date component.
    /// </summary>
    DateTime Today { get; }
}