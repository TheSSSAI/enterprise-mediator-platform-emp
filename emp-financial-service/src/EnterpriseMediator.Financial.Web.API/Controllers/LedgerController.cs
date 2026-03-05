using EnterpriseMediator.Financial.Application.Features.Ledger.DTOs;
using EnterpriseMediator.Financial.Application.Features.Ledger.Queries.GetTransactionHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMediator.Financial.Web.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "SystemAdministrator,FinanceManager")] // Strictly internal financial data
    [Produces("application/json")]
    public class LedgerController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<LedgerController> _logger;

        public LedgerController(ISender sender, ILogger<LedgerController> logger)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves the financial transaction ledger with optional filtering and pagination.
        /// </summary>
        /// <remarks>
        /// This endpoint provides a comprehensive view of all financial movements (payments, payouts, fees, refunds)
        /// recorded in the system. It supports filtering by date range, transaction type, and associated project or client.
        /// </remarks>
        /// <param name="startDate">Optional start date for filtering transactions (UTC).</param>
        /// <param name="endDate">Optional end date for filtering transactions (UTC).</param>
        /// <param name="type">Optional transaction type filter (e.g., ClientPayment, VendorPayout).</param>
        /// <param name="projectId">Optional Project ID to filter by.</param>
        /// <param name="clientId">Optional Client ID to filter by.</param>
        /// <param name="page">Page number (default 1).</param>
        /// <param name="pageSize">Items per page (default 20, max 100).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paginated list of transaction summaries.</returns>
        /// <response code="200">Returns the requested ledger data.</response>
        /// <response code="400">Invalid filter parameters.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TransactionSummaryDto>), StatusCodes.Status200OK)] // Simplified response type for documentation
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetTransactionHistory(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] string? type,
            [FromQuery] Guid? projectId,
            [FromQuery] Guid? clientId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            // Validate basic pagination inputs
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;
            if (pageSize > 100) pageSize = 100;

            // Validate date range if both provided
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                return BadRequest("Start date cannot be after end date.");
            }

            _logger.LogInformation("Retrieving transaction ledger. Page: {Page}, Size: {Size}, Project: {ProjectId}", 
                page, pageSize, projectId);

            // Construct the query object defined in the Application layer (Level 3)
            var query = new GetTransactionHistoryQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                TransactionType = type,
                ProjectId = projectId,
                ClientId = clientId,
                Page = page,
                PageSize = pageSize
            };

            try
            {
                // Execute Query via MediatR
                // Expecting a PagedResult<TransactionSummaryDto> or similar collection
                var result = await _sender.Send(query, cancellationToken);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid arguments provided for ledger query.");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving transaction ledger.");
                return StatusCode(500, new { error = "An error occurred while retrieving the ledger." });
            }
        }
    }
}