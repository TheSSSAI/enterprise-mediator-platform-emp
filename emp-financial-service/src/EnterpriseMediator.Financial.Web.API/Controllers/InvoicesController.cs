using EnterpriseMediator.Financial.Application.DTOs;
using EnterpriseMediator.Financial.Application.Features.Invoices.Commands.GenerateInvoice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseMediator.Financial.Web.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Requires authenticated user
    [Produces("application/json")]
    public class InvoicesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(ISender sender, ILogger<InvoicesController> logger)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Triggers the generation of a client invoice for a specific project.
        /// </summary>
        /// <remarks>
        /// This endpoint is typically called by System Administrators when a project is awarded.
        /// It calculates the invoice amount based on the project details and configuration,
        /// creates the invoice record, and generates a payment link via the payment gateway.
        /// </remarks>
        /// <param name="command">The invoice generation command containing project and client details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The ID of the generated invoice.</returns>
        /// <response code="201">Invoice successfully created.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden (requires appropriate role).</response>
        /// <response code="409">Conflict (e.g., invoice already exists).</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [Authorize(Roles = "SystemAdministrator,FinanceManager")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateInvoice(
            [FromBody] GenerateInvoiceCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received request to generate invoice for Project ID: {ProjectId}", command.ProjectId);

            try
            {
                // The handler returns a Result<Guid>. We assume a Result pattern is used in the Application layer.
                // Since we don't have the exact Result class definition visible here, we follow standard MediatR patterns.
                // Assuming the handler returns the Guid directly or throws exceptions for failures based on the Clean Architecture typical implementation.
                // If Result<T> is used, we would check IsSuccess. Here we wrap in try-catch for robustness.
                
                var invoiceId = await _sender.Send(command, cancellationToken);

                _logger.LogInformation("Successfully generated invoice {InvoiceId} for Project ID: {ProjectId}", invoiceId, command.ProjectId);

                return CreatedAtAction(nameof(GetInvoice), new { id = invoiceId }, invoiceId);
            }
            catch (ValidationException ex) // Assuming FluentValidation exception
            {
                _logger.LogWarning(ex, "Validation failed for invoice generation request for Project ID: {ProjectId}", command.ProjectId);
                return BadRequest(new { error = "Validation failed", details = ex.Message });
            }
            catch (InvalidOperationException ex) // Domain logic failure (e.g., invoice exists)
            {
                _logger.LogWarning(ex, "Business rule violation for invoice generation Project ID: {ProjectId}", command.ProjectId);
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error generating invoice for Project ID: {ProjectId}", command.ProjectId);
                return StatusCode(500, new { error = "An unexpected error occurred while processing the invoice." });
            }
        }

        /// <summary>
        /// Retrieves invoice details by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the invoice.</param>
        /// <returns>The invoice details.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "SystemAdministrator,FinanceManager")]
        [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInvoice(Guid id)
        {
            // Placeholder for query implementation.
            // In a full implementation, this would dispatch a GetInvoiceByIdQuery.
            // Returning 404 for now as the Query was not explicitly in the Level 3 file list provided,
            // but the endpoint is needed for CreatedAtAction consistency.
            await Task.CompletedTask;
            return NotFound("Invoice retrieval query not implemented in current scope.");
        }
    }
}