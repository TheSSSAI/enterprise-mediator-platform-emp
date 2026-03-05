using System;

namespace EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate
{
    /// <summary>
    /// Represents a specific financial rule determining when and how much a vendor is paid.
    /// Examples include "50% Upfront", "100% on Completion", or specific Milestone-based rules.
    /// This entity is part of the Project Aggregate.
    /// </summary>
    public class ProjectPayoutRule
    {
        /// <summary>
        /// Unique identifier for the payout rule.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The unique identifier of the project this rule belongs to.
        /// </summary>
        public Guid ProjectId { get; private set; }

        /// <summary>
        /// A descriptive name for the milestone or trigger (e.g., "Kickoff", "Final Delivery").
        /// </summary>
        public string MilestoneName { get; private set; }

        /// <summary>
        /// The percentage of the total project value allocated to this rule (0-100).
        /// </summary>
        public decimal Percentage { get; private set; }

        /// <summary>
        /// The sequence order in which this payout rule applies.
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Protected constructor for EF Core serialization.
        /// </summary>
        protected ProjectPayoutRule() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectPayoutRule"/> class.
        /// </summary>
        /// <param name="projectId">The project ID.</param>
        /// <param name="milestoneName">Name of the milestone.</param>
        /// <param name="percentage">Percentage of total value (0.0 to 100.0).</param>
        /// <param name="order">Sort order.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
        public ProjectPayoutRule(Guid projectId, string milestoneName, decimal percentage, int order)
        {
            if (projectId == Guid.Empty) 
                throw new ArgumentException("Payout rule must belong to a valid Project.", nameof(projectId));
            
            if (string.IsNullOrWhiteSpace(milestoneName)) 
                throw new ArgumentException("Milestone name cannot be empty.", nameof(milestoneName));
            
            if (percentage < 0 || percentage > 100) 
                throw new ArgumentException("Percentage must be between 0 and 100.", nameof(percentage));

            Id = Guid.NewGuid();
            ProjectId = projectId;
            MilestoneName = milestoneName;
            Percentage = percentage;
            Order = order;
        }

        /// <summary>
        /// Updates the percentage for this payout rule. 
        /// Used when rebalancing payout structures.
        /// </summary>
        /// <param name="newPercentage">The new percentage value.</param>
        public void UpdatePercentage(decimal newPercentage)
        {
            if (newPercentage < 0 || newPercentage > 100)
                throw new ArgumentException("Percentage must be between 0 and 100.", nameof(newPercentage));

            Percentage = newPercentage;
        }

        /// <summary>
        /// Calculates the monetary amount for this rule based on the total project value.
        /// </summary>
        /// <param name="totalProjectValue">The total cost of the project.</param>
        /// <returns>The calculated payout amount.</returns>
        public decimal CalculateAmount(decimal totalProjectValue)
        {
            if (totalProjectValue < 0)
                throw new ArgumentException("Total project value cannot be negative.", nameof(totalProjectValue));

            // Percentage is stored as whole number (e.g., 50 for 50%), so divide by 100.
            return Math.Round(totalProjectValue * (Percentage / 100m), 2);
        }
    }
}