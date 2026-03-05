using EnterpriseMediator.UserManagement.Application.Features.Internal.Queries.GetUserRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMediator.UserManagement.API.Controllers;

/// <summary>
/// Internal API controller exposing user management data to other microservices.
/// Protected by strict internal policies to prevent public access.
/// </summary>
[ApiController]
[Route("api/v1/internal/users")]
[Authorize(Policy = "InternalServicePolicy")] // Requires strict machine-to-machine auth
public class InternalUsersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<InternalUsersController> _logger;

    public InternalUsersController(ISender sender, ILogger<InternalUsersController> logger)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves the role of a user for RBAC checks in other services.
    /// Critical for cross-service authorization.
    /// </summary>
    /// <param name="id">The GUID of the user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The role name string.</returns>
    [HttpGet("{id}/role")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> GetUserRole(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Internal GetUserRole called with empty GUID");
            return BadRequest("Invalid User ID.");
        }

        try
        {
            _logger.LogDebug("Retrieving role for internal user check: {UserId}", id);
            
            var query = new GetUserRoleQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            // Based on GetUserRoleQuery logic, if user not found, it might throw NotFoundException 
            // handled by GlobalExceptionHandler, or return null/empty.
            // Assuming standard flow where NotFoundException is thrown for missing resources.
            
            return Ok(result);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            // GlobalExceptionHandler will catch this, but we log specific context here
            _logger.LogError(ex, "Failed to retrieve role for user {UserId} via internal API", id);
            throw;
        }
    }
}