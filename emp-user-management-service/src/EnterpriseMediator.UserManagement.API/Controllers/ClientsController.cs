using EnterpriseMediator.UserManagement.Application.Features.Clients.Commands.CreateClient;
using EnterpriseMediator.UserManagement.Application.Features.Clients.Queries.GetClientDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMediator.UserManagement.API.Controllers;

/// <summary>
/// Manages Client entity operations.
/// </summary>
[ApiController]
[Route("api/v1/clients")]
[Authorize] // Requires authenticated user
public class ClientsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(ISender sender, ILogger<ClientsController> logger)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new client profile.
    /// </summary>
    /// <param name="command">The client creation command payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ID of the newly created client.</returns>
    [HttpPost]
    [Authorize(Roles = "SystemAdmin")] // Only admins can onboard clients
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> CreateClient(
        [FromBody] CreateClientCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new client: {CompanyName}", command.CompanyName);

        var result = await _sender.Send(command, cancellationToken);

        // Assuming Result<T> pattern or direct return. 
        // If Application layer throws validation exceptions, Middleware handles 400.
        // If result indicates logical failure that isn't an exception, handle here.
        // Assuming direct return of Guid for simplicity based on provided context, or Result wrapper.
        
        // If result is wrapped in a Result class (common DDD pattern):
        // if (!result.IsSuccess) return BadRequest(result.Error);
        
        return CreatedAtAction(
            nameof(GetClient), 
            new { id = result }, 
            result);
    }

    /// <summary>
    /// Retrieves details for a specific client.
    /// </summary>
    /// <param name="id">The client ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Client details DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "SystemAdmin,FinanceManager")] // Restricted access
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetClient(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty) return BadRequest("Invalid Client ID");

        var query = new GetClientDetailsQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }
}