using MediatR;
using System;

namespace EnterpriseMediator.UserManagement.Application.Features.Internal.Queries.GetUserRole;

/// <summary>
/// Query to retrieve the primary role name for a specific user.
/// Used primarily by internal microservice communication to enforce RBAC.
/// </summary>
/// <param name="UserId">The unique identifier of the user.</param>
public sealed record GetUserRoleQuery(Guid UserId) : IRequest<string>;