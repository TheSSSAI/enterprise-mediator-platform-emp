using System;
using EnterpriseMediator.Financial.Domain.ValueObjects;

namespace EnterpriseMediator.Financial.Domain.Events
{
    /// <summary>
    /// Marker interface for domain events to ensure type safety across the domain layer.
    /// </summary>
    public interface IDomainEvent { }

    /// <summary>
    /// Event raised when an invoice is successfully paid.
    /// This triggers downstream effects such as updating project status to 'Active'.
    /// </summary>
    public class InvoicePaidEvent : IDomainEvent
    {
        public Guid InvoiceId { get; }
        public Guid ProjectId { get; }
        public Money AmountPaid { get; }
        public DateTime PaidAt { get; }
        public string TransactionReference { get; }

        public InvoicePaidEvent(Guid invoiceId, Guid projectId, Money amountPaid, DateTime paidAt, string transactionReference)
        {
            if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId cannot be empty", nameof(invoiceId));
            if (projectId == Guid.Empty) throw new ArgumentException("ProjectId cannot be empty", nameof(projectId));
            if (string.IsNullOrWhiteSpace(transactionReference)) throw new ArgumentException("TransactionReference cannot be empty", nameof(transactionReference));

            InvoiceId = invoiceId;
            ProjectId = projectId;
            AmountPaid = amountPaid ?? throw new ArgumentNullException(nameof(amountPaid));
            PaidAt = paidAt;
            TransactionReference = transactionReference;
        }
    }
}