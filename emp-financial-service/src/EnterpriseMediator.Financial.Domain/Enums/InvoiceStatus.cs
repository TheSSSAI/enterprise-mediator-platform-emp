namespace EnterpriseMediator.Financial.Domain.Enums;

/// <summary>
/// Defines the lifecycle states of an invoice within the financial system.
/// </summary>
public enum InvoiceStatus
{
    /// <summary>
    /// The invoice has been created but not yet finalized or sent to the client.
    /// It can still be modified or deleted.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// The invoice has been finalized and a payment link/notification has been sent to the client.
    /// It is now awaiting payment and cannot be modified without being voided.
    /// </summary>
    Sent = 1,

    /// <summary>
    /// Payment has been successfully captured and reconciled.
    /// This state triggers the project to move to an 'Active' status.
    /// </summary>
    Paid = 2,

    /// <summary>
    /// Payment was not received by the defined due date.
    /// </summary>
    Overdue = 3,

    /// <summary>
    /// The invoice has been voided or cancelled by an administrator.
    /// No payment can be accepted for this invoice.
    /// </summary>
    Cancelled = 4
}