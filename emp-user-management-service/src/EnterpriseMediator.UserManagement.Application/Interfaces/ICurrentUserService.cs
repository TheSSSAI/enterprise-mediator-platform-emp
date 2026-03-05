using System;
using System.Collections.Generic;

namespace EnterpriseMediator.UserManagement.Application.Interfaces;

/// <summary>
/// Provides access to the current authenticated user's information.
/// Implementation typically wraps IHttpContextAccessor.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the unique identifier of the current user. 
    /// Returns Guid.Empty if the user is not authenticated.
    /// </summary>
    Guid UserId { get; }

    /// <summary>
    /// Gets the email address of the current user.
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Gets the list of roles assigned to the current user.
    /// </summary>
    IReadOnlyList<string> Roles { get; }

    /// <summary>
    /// Gets a value indicating whether the user is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Gets the tenant/company ID from the user claims if available.
    /// </summary>
    Guid? TenantId { get; }
}