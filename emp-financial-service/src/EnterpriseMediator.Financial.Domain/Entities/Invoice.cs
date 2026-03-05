using System;
using System.Collections.Generic;
using EnterpriseMediator.Financial.Domain.Enums;
using EnterpriseMediator.Financial.Domain.Events;
using EnterpriseMediator.Financial.Domain.ValueObjects;

namespace EnterpriseMediator.Financial.Domain.Entities
{
    /// <summary>
    /// Represents an invoice issued to a client.
    /// Acts as an Aggregate Root responsible for managing the lifecycle of client payments.
    /// </summary>
    public class Invoice
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid ClientId { get; private set; }
        public Money TotalAmount { get; private set; }
        public InvoiceStatus Status { get; private set; }
        public string? StripePaymentIntentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }
        
        // Optimistic concurrency control
        public byte[] RowVersion { get; private set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        // EF Core Constructor
        protected Invoice() { }

        private Invoice(Guid projectId, Guid clientId, Money totalAmount)
        {
            if (projectId == Guid.Empty) throw new ArgumentException("Project ID is required.", nameof(projectId));
            if (clientId == Guid.Empty) throw new ArgumentException("Client ID is required.", nameof(clientId));
            if (totalAmount == null) throw new ArgumentNullException(nameof(totalAmount));
            if (totalAmount.Amount <= 0) throw new ArgumentException("Invoice amount must be positive.", nameof(totalAmount));

            Id = Guid.NewGuid();
            ProjectId = projectId;
            ClientId = clientId;
            TotalAmount = totalAmount;
            Status = InvoiceStatus.Draft;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Factory method to create a new draft invoice.
        /// </summary>
        public static Invoice Create(Guid projectId, Guid clientId, Money amount)
        {
            return new Invoice(projectId, clientId, amount);
        }

        /// <summary>
        /// Associates the invoice with a Stripe Payment Intent and marks it as Sent.
        /// </summary>
        public void SetPaymentIntent(string paymentIntentId)
        {
            if (string.IsNullOrWhiteSpace(paymentIntentId))
                throw new ArgumentException("Payment Intent ID cannot be empty.", nameof(paymentIntentId));

            if (Status != InvoiceStatus.Draft && Status != InvoiceStatus.Sent)
                throw new InvalidOperationException($"Cannot set payment intent for invoice in status {Status}.");

            StripePaymentIntentId = paymentIntentId;
            Status = InvoiceStatus.Sent;
        }

        /// <summary>
        /// Transitions the invoice to Paid state upon successful payment confirmation.
        /// </summary>
        public void MarkAsPaid(string transactionReference, DateTime paidAt)
        {
            if (Status == InvoiceStatus.Paid) return; // Idempotency check

            if (Status == InvoiceStatus.Cancelled)
                throw new InvalidOperationException("Cannot pay a cancelled invoice.");

            Status = InvoiceStatus.Paid;
            PaidAt = paidAt;

            // Raise Domain Event
            _domainEvents.Add(new InvoicePaidEvent(Id, ProjectId, TotalAmount, paidAt, transactionReference));
        }

        /// <summary>
        /// Marks the invoice as cancelled/voided.
        /// </summary>
        public void Cancel()
        {
            if (Status == InvoiceStatus.Paid)
                throw new InvalidOperationException("Cannot cancel an invoice that has already been paid.");

            Status = InvoiceStatus.Cancelled;
        }

        /// <summary>
        /// Clears domain events after they have been dispatched.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}