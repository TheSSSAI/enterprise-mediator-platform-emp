using System;
using System.Collections.Generic;
using EnterpriseMediator.Financial.Domain.Events;
using EnterpriseMediator.Financial.Domain.ValueObjects;

namespace EnterpriseMediator.Financial.Domain.Entities
{
    /// <summary>
    /// Represents the status of a payout request.
    /// Defined here to ensure self-contained domain logic for this aggregate.
    /// </summary>
    public enum PayoutStatus
    {
        PendingApproval = 0,
        Approved = 1,
        Processing = 2,
        Paid = 3,
        Failed = 4,
        Rejected = 5
    }

    /// <summary>
    /// Represents a payout request to a vendor.
    /// Managed via a strict approval workflow (Initiate -> Approve -> Process).
    /// </summary>
    public class Payout
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public Guid Id { get; private set; }
        public Guid VendorId { get; private set; }
        public Guid ProjectId { get; private set; }
        public Money Amount { get; private set; }
        public PayoutStatus Status { get; private set; }
        
        public string? WiseTransferId { get; private set; }
        public Guid? ApproverId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public string? FailureReason { get; private set; }

        // Optimistic concurrency control
        public byte[] RowVersion { get; private set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        // EF Core Constructor
        protected Payout() { }

        private Payout(Guid vendorId, Guid projectId, Money amount)
        {
            if (vendorId == Guid.Empty) throw new ArgumentException("Vendor ID is required.", nameof(vendorId));
            if (projectId == Guid.Empty) throw new ArgumentException("Project ID is required.", nameof(projectId));
            if (amount == null) throw new ArgumentNullException(nameof(amount));
            if (amount.Amount <= 0) throw new ArgumentException("Payout amount must be positive.", nameof(amount));

            Id = Guid.NewGuid();
            VendorId = vendorId;
            ProjectId = projectId;
            Amount = amount;
            Status = PayoutStatus.PendingApproval;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Initiates a new payout request which requires approval.
        /// </summary>
        public static Payout Initiate(Guid vendorId, Guid projectId, Money amount)
        {
            return new Payout(vendorId, projectId, amount);
        }

        /// <summary>
        /// Approves the payout for processing.
        /// Must be performed by an authorized finance manager.
        /// </summary>
        public void Approve(Guid approverId)
        {
            if (Status != PayoutStatus.PendingApproval)
                throw new InvalidOperationException($"Cannot approve payout in status {Status}.");
            
            if (approverId == Guid.Empty)
                throw new ArgumentException("Approver ID is required.", nameof(approverId));

            Status = PayoutStatus.Approved;
            ApproverId = approverId;
        }

        /// <summary>
        /// Marks the payout as processing after being submitted to the gateway (Wise).
        /// </summary>
        public void MarkAsProcessing(string externalTransferId)
        {
            if (string.IsNullOrWhiteSpace(externalTransferId))
                throw new ArgumentException("External transfer ID is required.", nameof(externalTransferId));

            if (Status != PayoutStatus.Approved)
                throw new InvalidOperationException($"Cannot process payout. Current status: {Status}. Payout must be Approved first.");

            WiseTransferId = externalTransferId;
            Status = PayoutStatus.Processing;
        }

        /// <summary>
        /// Completes the payout lifecycle upon confirmation from the gateway.
        /// </summary>
        public void MarkAsPaid(DateTime processedAt)
        {
            if (Status != PayoutStatus.Processing && Status != PayoutStatus.Approved)
                throw new InvalidOperationException($"Cannot mark payout as paid from status {Status}.");

            Status = PayoutStatus.Paid;
            ProcessedAt = processedAt;

            // Raise domain event to notify other services (e.g. Notification Service)
            _domainEvents.Add(new PayoutProcessedEvent(
                Id, 
                VendorId, 
                ProjectId, 
                Amount, 
                processedAt, 
                WiseTransferId ?? "UNKNOWN"
            ));
        }

        /// <summary>
        /// Marks the payout as failed.
        /// </summary>
        public void MarkAsFailed(string reason)
        {
            if (Status == PayoutStatus.Paid)
                throw new InvalidOperationException("Cannot fail a payout that is already paid.");

            Status = PayoutStatus.Failed;
            FailureReason = reason;
        }

        /// <summary>
        /// Rejects the payout request.
        /// </summary>
        public void Reject(Guid rejectorId, string reason)
        {
            if (Status != PayoutStatus.PendingApproval)
                throw new InvalidOperationException("Only pending payouts can be rejected.");

            Status = PayoutStatus.Rejected;
            ApproverId = rejectorId; // Tracking who rejected it
            FailureReason = reason;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}