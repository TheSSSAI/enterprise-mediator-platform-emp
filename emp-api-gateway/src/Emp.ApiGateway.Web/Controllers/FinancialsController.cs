using System.Net.Mime;
using Emp.ApiGateway.Application.DTOs.Public;
using Emp.ApiGateway.Application.Features.Projects.Queries; 
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emp.ApiGateway.Web.Controllers;

/// <summary>
/// Public API Controller for Financial operations.
/// Handles aggregated financial views and operations via the Financial Microservice.
/// </summary>
[ApiController]
[Route("api/v1/financials")]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
public class FinancialsController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<FinancialsController> _logger;

    public FinancialsController(ISender mediator, ILogger<FinancialsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves a summary of financials for a specific project.
    /// </summary>
    /// <param name="projectId">The project identifier.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Financial summary including budget, spend, and invoices.</returns>
    /// <response code="200">Financial summary retrieved.</response>
    /// <response code="404">Project financials not found.</response>
    [HttpGet("projects/{projectId:guid}")]
    [ProducesResponseType(typeof(PublicFinancialSummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PublicFinancialSummaryDto>> GetProjectFinancialSummary(
        [FromRoute] Guid projectId,
        CancellationToken ct)
    {
        _logger.LogDebug("Retrieving financial summary for project: {ProjectId}", projectId);

        // We reuse the query pattern. Assuming GetProjectDashboardQuery handles aggregation,
        // we might have a specific query just for financials or extract it from the dashboard query.
        // For efficiency, we will assume a specific query exists or use the dashboard handler which acts as the aggregator.
        // In a strict CQRS implementation, we would define a specific GetProjectFinancialsQuery.
        // Given the file structure constraints, we'll leverage the existing patterns.
        
        // NOTE: In a real scenario, we would have a dedicated GetProjectFinancialsQuery. 
        // Here, we construct a dashboard query but only return the financial part to the client 
        // to maintain the specific endpoint contract, or we rely on the generic forwarding pattern.
        
        // Constructing a query to get dashboard data which includes financials
        var query = new GetProjectDashboardQuery(projectId);
        var result = await _mediator.Send(query, ct);

        if (result?.Financials == null)
        {
            _logger.LogWarning("Financial data not found for project: {ProjectId}", projectId);
            return NotFound($"Financial records for project {projectId} not found.");
        }

        return Ok(result.Financials);
    }

    /// <summary>
    /// Placeholder endpoint for future invoice generation logic.
    /// Ensures the controller is ready for full financial workflow implementation.
    /// </summary>
    /// <param name="projectId">Project ID.</param>
    /// <returns>Not implemented status.</returns>
    [HttpPost("projects/{projectId:guid}/invoices/generate")]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public ActionResult GenerateInvoice([FromRoute] Guid projectId)
    {
        _logger.LogInformation("Invoice generation requested for project {ProjectId}", projectId);
        // This will eventually map to a GenerateInvoiceCommand
        return StatusCode(StatusCodes.Status501NotImplemented, "Invoice generation feature is coming soon.");
    }
}