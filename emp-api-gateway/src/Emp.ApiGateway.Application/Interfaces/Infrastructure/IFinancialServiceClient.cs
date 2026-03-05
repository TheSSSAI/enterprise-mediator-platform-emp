using System;
using System.Threading;
using System.Threading.Tasks;

namespace Emp.ApiGateway.Application.Interfaces.Infrastructure
{
    /// <summary>
    /// Contract for communicating with the downstream Financial Microservice.
    /// Handles aggregation of financial data for projects.
    /// </summary>
    public interface IFinancialServiceClient
    {
        /// <summary>
        /// Retrieves the financial summary for a specific project, including budget, invoiced amounts, and payouts.
        /// </summary>
        /// <param name="projectId">The unique identifier of the project.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Financial summary DTO if available, otherwise null.</returns>
        Task<FinancialSummaryDto?> GetProjectFinancialSummaryAsync(Guid projectId, CancellationToken ct);
    }

    /// <summary>
    /// Data Transfer Object representing the financial state of a project.
    /// </summary>
    public record FinancialSummaryDto
    {
        public Guid ProjectId { get; init; }
        public decimal TotalBudget { get; init; }
        public decimal TotalInvoiced { get; init; }
        public decimal TotalPaid { get; init; }
        public decimal PendingPayouts { get; init; }
        public string Currency { get; init; } = "USD";
        public bool HasOverdueInvoices { get; init; }
    }
}