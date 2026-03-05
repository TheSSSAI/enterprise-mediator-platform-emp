using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Financials.Enums;
using EnterpriseMediator.Domain.Financials.Events;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.Shared.ValueObjects;
using EnterpriseMediator.Domain.ClientManagement.Aggregates;

namespace EnterpriseMediator.Domain.Financials.Aggregates
{
    public class Invoice : AggregateRoot<InvoiceId>
    {
        private readonly List<InvoiceLineItem> _items = new();

        public ProjectId ProjectId { get; private set; }
        public ClientId ClientId { get; private set; }
        
        public string InvoiceNumber { get; private set; }
        public DateTimeOffset IssueDate { get; private set; }
        public DateTimeOffset DueDate { get; private set; }
        
        public InvoiceStatus Status { get; private set; }
        
        // Calculated totals
        public Money SubTotal => _items.Count > 0 
            ? new Money(_items.Sum(i => i.Total.Amount), _items.First().Total.Currency) 
            : new Money(0, Currency.USD); 
            
        public Money TaxAmount { get; private set; }
        public Money TotalAmount => SubTotal + TaxAmount;

        public IReadOnlyCollection<InvoiceLineItem> Items => _items.AsReadOnly();

        // EF Core
        #pragma warning disable CS8618
        private Invoice() { }
        #pragma warning restore CS8618

        private Invoice(InvoiceId id, ProjectId projectId, ClientId clientId, string invoiceNumber, DateTimeOffset issueDate, DateTimeOffset dueDate, Currency currency)
        {
            Id = id;
            ProjectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            InvoiceNumber = !string.IsNullOrWhiteSpace(invoiceNumber) ? invoiceNumber : throw new BusinessRuleValidationException("Invoice Number is required.");
            
            if (dueDate < issueDate) throw new BusinessRuleValidationException("Due date cannot be before issue date.");
            
            IssueDate = issueDate;
            DueDate = dueDate;
            Status = InvoiceStatus.Draft;
            TaxAmount = new Money(0, currency);
        }

        public static Invoice Create(ProjectId projectId, ClientId clientId, string invoiceNumber, DateTimeOffset dueDate, Currency currency)
        {
            return new Invoice(new InvoiceId(Guid.NewGuid()), projectId, clientId, invoiceNumber, DateTimeOffset.UtcNow, dueDate, currency);
        }

        public void AddLineItem(string description, decimal quantity, Money unitPrice)
        {
            if (Status != InvoiceStatus.Draft) throw new BusinessRuleValidationException("Cannot modify items of a non-draft invoice.");
            if (unitPrice.Currency != TaxAmount.Currency) throw new BusinessRuleValidationException("Line item currency must match invoice currency.");

            _items.Add(new InvoiceLineItem(description, quantity, unitPrice));
        }

        public void SetTax(Money tax)
        {
            if (Status != InvoiceStatus.Draft) throw new BusinessRuleValidationException("Cannot modify tax of a non-draft invoice.");
            if (tax.Currency != SubTotal.Currency) throw new BusinessRuleValidationException("Tax currency must match invoice currency.");
            
            TaxAmount = tax;
        }

        public void Issue()
        {
            if (Status != InvoiceStatus.Draft) throw new BusinessRuleValidationException("Invoice is already issued.");
            if (_items.Count == 0) throw new BusinessRuleValidationException("Cannot issue an empty invoice.");

            Status = InvoiceStatus.Issued;
        }

        public void MarkAsPaid(DateTimeOffset paidAt, string transactionReference)
        {
            if (Status != InvoiceStatus.Issued && Status != InvoiceStatus.Overdue)
            {
                throw new BusinessRuleValidationException("Only Issued or Overdue invoices can be marked as paid.");
            }

            Status = InvoiceStatus.Paid;
            AddDomainEvent(new InvoicePaidDomainEvent(Id, ProjectId, TotalAmount, paidAt, transactionReference));
        }

        public void MarkAsOverdue()
        {
            if (Status == InvoiceStatus.Issued && DateTimeOffset.UtcNow > DueDate)
            {
                Status = InvoiceStatus.Overdue;
            }
        }

        public void Cancel(string reason)
        {
            if (Status == InvoiceStatus.Paid) throw new BusinessRuleValidationException("Cannot cancel a paid invoice.");
            
            Status = InvoiceStatus.Cancelled;
        }
    }

    public class InvoiceLineItem : Entity<Guid>
    {
        public string Description { get; private set; }
        public decimal Quantity { get; private set; }
        public Money UnitPrice { get; private set; }
        public Money Total => UnitPrice * Quantity;

        internal InvoiceLineItem(string description, decimal quantity, Money unitPrice)
        {
            Id = Guid.NewGuid();
            Description = !string.IsNullOrWhiteSpace(description) ? description : throw new BusinessRuleValidationException("Item description is required.");
            if (quantity <= 0) throw new BusinessRuleValidationException("Quantity must be greater than zero.");
            
            Quantity = quantity;
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
        }
        
        #pragma warning disable CS8618
        private InvoiceLineItem() { }
        #pragma warning restore CS8618
    }
}