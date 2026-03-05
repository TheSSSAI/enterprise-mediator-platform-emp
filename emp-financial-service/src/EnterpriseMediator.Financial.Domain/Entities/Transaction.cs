using System;
using EnterpriseMediator.Financial.Domain.Enums;
using EnterpriseMediator.Financial.Domain.ValueObjects;

namespace EnterpriseMediator.Financial.Domain.Entities
{
    /// <summary>
    /// Represents an immutable financial record in the ledger.
    /// Tracks all money movement (Payments, Payouts, Refunds, Fees).
    /// </summary>
    public class Transaction
    {
        public Guid Id { get; private set; }
        public TransactionType Type { get; private set; }
        public Money Amount { get; private set; }
        public DateTime Timestamp { get; private set; }
        
        // Contextual Metadata
        public Guid ProjectId { get; private set; }
        public Guid? InvoiceId { get; private set; }
        public Guid? PayoutId { get; private set; }
        public string ExternalReferenceId { get; private set; }
        public string? Description { get; private set; }

        // EF Core Constructor
        protected Transaction() { }

        private Transaction(
            TransactionType type,
            Money amount,
            Guid projectId,
            string externalReferenceId,
            Guid? invoiceId = null,
            Guid? payoutId = null,
            string? description = null)
        {
            if (projectId == Guid.Empty) throw new ArgumentException("Project ID is required for ledger integrity.", nameof(projectId));
            if (amount == null) throw new ArgumentNullException(nameof(amount));
            if (string.IsNullOrWhiteSpace(externalReferenceId)) throw new ArgumentException("External reference ID is required.", nameof(externalReferenceId));

            Id = Guid.NewGuid();
            Type = type;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
            ProjectId = projectId;
            ExternalReferenceId = externalReferenceId;
            InvoiceId = invoiceId;
            PayoutId = payoutId;
            Description = description;
        }

        /// <summary>
        /// Records an incoming payment from a client.
        /// </summary>
        public static Transaction RecordPayment(Invoice invoice, string externalTransactionId, string? description = null)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            return new Transaction(
                TransactionType.ClientPayment,
                invoice.TotalAmount,
                invoice.ProjectId,
                externalTransactionId,
                invoiceId: invoice.Id,
                description: description ?? $"Payment for Invoice {invoice.Id}"
            );
        }

        /// <summary>
        /// Records an outgoing payout to a vendor.
        /// </summary>
        public static Transaction RecordPayout(Payout payout, string externalTransferId, string? description = null)
        {
            if (payout == null) throw new ArgumentNullException(nameof(payout));

            return new Transaction(
                TransactionType.VendorPayout,
                payout.Amount,
                payout.ProjectId,
                externalTransferId,
                payoutId: payout.Id,
                description: description ?? $"Payout for Project {payout.ProjectId}"
            );
        }

        /// <summary>
        /// Records a refund issued to a client.
        /// </summary>
        public static Transaction RecordRefund(Guid projectId, Money amount, Guid originalInvoiceId, string externalRefundId, string reason)
        {
            return new Transaction(
                TransactionType.Refund,
                amount,
                projectId,
                externalRefundId,
                invoiceId: originalInvoiceId,
                description: $"Refund: {reason}"
            );
        }

        /// <summary>
        /// Records a platform fee retained from a transaction.
        /// </summary>
        public static Transaction RecordFee(Guid projectId, Money amount, string referenceId)
        {
            return new Transaction(
                TransactionType.PlatformFee,
                amount,
                projectId,
                referenceId,
                description: "Platform Service Fee"
            );
        }
    }
}