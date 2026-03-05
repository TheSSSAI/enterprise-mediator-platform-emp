using System.Net.Mime;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emp.ApiGateway.Web.Controllers;

/// <summary>
/// Public API Controller for User-related operations.
/// Handles current user context, permissions, and profile aggregation.
/// </summary>
[ApiController]
[Route("api/v1/users")]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ISender mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves the profile information for the currently authenticated user.
    /// Extracts identity from the JWT claims.
    /// </summary>
    /// <returns>User profile details.</returns>
    /// <response code="200">User details retrieved successfully.</response>
    /// <response code="401">User is not authenticated.</response>
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<object> GetCurrentUser()
    {
        // In a BFF pattern, we often need to echo back who the user is based on their token
        // so the frontend can bootstrap the application state (name, roles, etc.)
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
        var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value ?? User.Identity?.Name;
        
        // Extract roles (Cognito groups often map to "cognito:groups" or standard Role claims)
        var roles = User.FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .Union(User.FindAll("cognito:groups").Select(c => c.Value))
            .Distinct()
            .ToList();

        if (string.IsNullOrEmpty(userIdClaim))
        {
            _logger.LogWarning("Authenticated user request missing NameIdentifier claim.");
            return Unauthorized("User identity could not be verified.");
        }

        _logger.LogDebug("Retrieving context for user: {UserId}", userIdClaim);

        // Construct a simple profile object from claims. 
        // In a more complex scenario, this might trigger a GetUserProfileQuery to the User Service
        // to fetch extended profile data not present in the token.
        var userProfile = new
        {
            Id = userIdClaim,
            Email = emailClaim,
            Name = nameClaim,
            Roles = roles,
            IsAuthenticated = true
        };

        return Ok(userProfile);
    }

    /// <summary>
    /// checks if the current user has specific permissions.
    /// Useful for frontend feature toggling.
    /// </summary>
    /// <param name="permission">The permission code to check.</param>
    /// <returns>Boolean indicating access.</returns>
    [HttpGet("permissions/check")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public ActionResult<bool> CheckPermission([FromQuery] string permission)
    {
        if (string.IsNullOrWhiteSpace(permission))
        {
            return BadRequest("Permission string cannot be empty.");
        }

        // Implementation would check against the User's claims or a permissions cache
        // For MVP/BFF, we check if the permission exists in the claims
        var hasPermission = User.HasClaim(c => c.Type == "permissions" && c.Value == permission);
        
        return Ok(hasPermission);
    }
}