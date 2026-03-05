using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseMediator.UserManagement.Application.Interfaces;

/// <summary>
/// Defines the contract for internal user service operations exposed to other microservices.
/// This interface abstracts the logic for retrieving user data required by external contexts.
/// </summary>
public interface IInternalUserService
{
    /// <summary>
    /// Retrieves the primary role name for a specific user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The name of the user's primary role.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the user does not exist.</exception>
    Task<string> GetUserRoleAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user exists and is active.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the user exists and is active, otherwise false.</returns>
    Task<bool> IsUserActiveAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the tenant/company ID associated with the user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The Guid of the Client or Vendor company, or null if internal.</returns>
    Task<Guid?> GetUserCompanyIdAsync(Guid userId, CancellationToken cancellationToken = default);
}