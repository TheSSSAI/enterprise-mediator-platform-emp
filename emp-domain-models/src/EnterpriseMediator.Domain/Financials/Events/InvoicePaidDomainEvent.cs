using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Financials.Aggregates;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.Financials.Events;

/// <summary>
/// Domain event raised when an invoice is fully paid by the client.
/// This event typically triggers the project status to transition to Active.
/// </summary>
/// <param name="InvoiceId">The unique identifier of the paid invoice.</param>
/// <param name="ProjectId">The project associated with the invoice.</param>
/// <param name="PaymentAmount">The total amount paid.</param>
/// <param name="TransactionReference">External reference ID (e.g., Stripe PaymentIntent ID).</param>
/// <param name="OccurredOn">Timestamp of the event.</param>
public record InvoicePaidDomainEvent(
    InvoiceId InvoiceId,
    ProjectId ProjectId,
    Money PaymentAmount,
    string TransactionReference,
    DateTime OccurredOn
) : IDomainEvent;