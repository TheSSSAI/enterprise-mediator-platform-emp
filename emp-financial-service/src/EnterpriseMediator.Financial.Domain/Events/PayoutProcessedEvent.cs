using System;
using EnterpriseMediator.Financial.Domain.ValueObjects;

namespace EnterpriseMediator.Financial.Domain.Events
{
    /// <summary>
    /// Event raised when a vendor payout is successfully processed by the payment gateway.
    /// This signals the completion of the funds transfer cycle for a specific milestone or project.
    /// </summary>
    public class PayoutProcessedEvent : IDomainEvent
    {
        public Guid PayoutId { get; }
        public Guid VendorId { get; }
        public Guid ProjectId { get; }
        public Money PayoutAmount { get; }
        public DateTime ProcessedAt { get; }
        public string ExternalTransferId { get; }

        public PayoutProcessedEvent(Guid payoutId, Guid vendorId, Guid projectId, Money payoutAmount, DateTime processedAt, string externalTransferId)
        {
            if (payoutId == Guid.Empty) throw new ArgumentException("PayoutId cannot be empty", nameof(payoutId));
            if (vendorId == Guid.Empty) throw new ArgumentException("VendorId cannot be empty", nameof(vendorId));
            if (projectId == Guid.Empty) throw new ArgumentException("ProjectId cannot be empty", nameof(projectId));
            if (string.IsNullOrWhiteSpace(externalTransferId)) throw new ArgumentException("ExternalTransferId cannot be empty", nameof(externalTransferId));

            PayoutId = payoutId;
            VendorId = vendorId;
            ProjectId = projectId;
            PayoutAmount = payoutAmount ?? throw new ArgumentNullException(nameof(payoutAmount));
            ProcessedAt = processedAt;
            ExternalTransferId = externalTransferId;
        }
    }
}