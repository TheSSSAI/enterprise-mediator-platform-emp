using EnterpriseMediator.UserManagement.Application.Features.Vendors.Commands.CreateVendor;
using EnterpriseMediator.UserManagement.Application.Features.Vendors.Commands.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMediator.UserManagement.API.Controllers;

/// <summary>
/// Manages Vendor entity operations including onboarding and profile management.
/// </summary>
[ApiController]
[Route("api/v1/vendors")]
[Authorize]
public class VendorsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<VendorsController> _logger;

    public VendorsController(ISender sender, ILogger<VendorsController> logger)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new vendor profile.
    /// </summary>
    /// <param name="command">Vendor creation payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ID of the created vendor.</returns>
    [HttpPost]
    [Authorize(Roles = "SystemAdmin")] // Only admins initiate vendor onboarding
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> CreateVendor(
        [FromBody] CreateVendorCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new vendor: {CompanyName}", command.CompanyName);

        var result = await _sender.Send(command, cancellationToken);

        // Returns 201 with the location header pointing to a detail endpoint (if it existed in scope)
        // Since GetVendorDetails isn't explicitly in the file list but is implied by REST standards,
        // we use a generic URI or the ID itself.
        return Created($"/api/v1/vendors/{result}", result);
    }

    /// <summary>
    /// Updates an existing vendor's profile information.
    /// Supports self-service updates by vendors or admin overrides.
    /// </summary>
    /// <param name="id">The vendor ID to update.</param>
    /// <param name="command">The update payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [Authorize(Roles = "SystemAdmin,Vendor")] // Admins or the Vendor themselves
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfile(
        Guid id, 
        [FromBody] UpdateVendorProfileCommand command, 
        CancellationToken cancellationToken)
    {
        if (id != command.VendorId)
        {
            return BadRequest("Route ID does not match command ID.");
        }

        // Note: Resource-based authorization (ensuring a Vendor user only updates THEIR vendor profile)
        // should be handled within the Application Layer (Command Handler) or via an Authorization Policy.
        // The Controller assumes the Command Handler enforces "Current User" checks.

        _logger.LogInformation("Updating profile for vendor: {VendorId}", id);

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
}