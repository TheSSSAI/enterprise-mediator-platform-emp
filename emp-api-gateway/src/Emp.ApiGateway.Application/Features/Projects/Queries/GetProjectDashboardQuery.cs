using System;
using MediatR;
using Emp.ApiGateway.Application.Interfaces.Infrastructure;

namespace Emp.ApiGateway.Application.Features.Projects.Queries
{
    /// <summary>
    /// Query to retrieve an aggregated dashboard view for a specific project.
    /// Combines data from Project and Financial services.
    /// </summary>
    public record GetProjectDashboardQuery(Guid ProjectId) : IRequest<ProjectDashboardResponse>;

    /// <summary>
    /// Aggregated response for the project dashboard.
    /// </summary>
    public record ProjectDashboardResponse
    {
        /// <summary>
        /// Core project details.
        /// </summary>
        public PublicProjectDto? Project { get; init; }

        /// <summary>
        /// Financial overview data.
        /// </summary>
        public PublicFinancialSummaryDto? Financials { get; init; }
    }

    /// <summary>
    /// Public facing project DTO for the dashboard.
    /// </summary>
    public record PublicProjectDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }

    /// <summary>
    /// Public facing financial summary DTO for the dashboard.
    /// </summary>
    public record PublicFinancialSummaryDto
    {
        public decimal TotalBudget { get; init; }
        public decimal TotalSpent { get; init; }
        public string Currency { get; init; } = "USD";
        public string FinancialHealthStatus { get; init; } = "Unknown";
    }
}