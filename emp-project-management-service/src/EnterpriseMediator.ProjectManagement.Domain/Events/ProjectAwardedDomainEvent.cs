using System;

namespace EnterpriseMediator.ProjectManagement.Domain.Events
{
    /// <summary>
    /// Domain Event raised when a project is officially awarded to a vendor proposal.
    /// Triggers downstream processes such as Invoice Generation and Notification Dispatch.
    /// </summary>
    public sealed record ProjectAwardedDomainEvent
    {
        /// <summary>
        /// Unique identifier for this specific event instance.
        /// </summary>
        public Guid EventId { get; init; }

        /// <summary>
        /// The UTC timestamp when the awarding action occurred.
        /// </summary>
        public DateTimeOffset OccurredOn { get; init; }

        /// <summary>
        /// The ID of the project being awarded.
        /// </summary>
        public Guid ProjectId { get; init; }

        /// <summary>
        /// The ID of the specific proposal that was accepted.
        /// </summary>
        public Guid ProposalId { get; init; }

        /// <summary>
        /// The ID of the vendor who won the project.
        /// </summary>
        public Guid VendorId { get; init; }

        /// <summary>
        /// The agreed financial amount for the project award.
        /// </summary>
        public decimal AwardedAmount { get; init; }

        /// <summary>
        /// The currency code (ISO 4217) for the awarded amount.
        /// </summary>
        public string Currency { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectAwardedDomainEvent"/> record.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="proposalId">The winning proposal identifier.</param>
        /// <param name="vendorId">The winning vendor identifier.</param>
        /// <param name="awardedAmount">The total agreed cost.</param>
        /// <param name="currency">The currency code.</param>
        /// <exception cref="ArgumentException">Thrown if IDs are empty or amount is negative.</exception>
        public ProjectAwardedDomainEvent(
            Guid projectId, 
            Guid proposalId, 
            Guid vendorId, 
            decimal awardedAmount, 
            string currency)
        {
            if (projectId == Guid.Empty) throw new ArgumentException("ProjectId cannot be empty.", nameof(projectId));
            if (proposalId == Guid.Empty) throw new ArgumentException("ProposalId cannot be empty.", nameof(proposalId));
            if (vendorId == Guid.Empty) throw new ArgumentException("VendorId cannot be empty.", nameof(vendorId));
            if (awardedAmount < 0) throw new ArgumentException("Awarded amount cannot be negative.", nameof(awardedAmount));
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency code is required.", nameof(currency));

            EventId = Guid.NewGuid();
            OccurredOn = DateTimeOffset.UtcNow;
            ProjectId = projectId;
            ProposalId = proposalId;
            VendorId = vendorId;
            AwardedAmount = awardedAmount;
            Currency = currency;
        }
    }
}