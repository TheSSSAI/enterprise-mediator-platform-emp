namespace EnterpriseMediator.Financial.Domain.Enums;

/// <summary>
/// Classifies financial ledger entries for reporting, auditing, and reconciliation purposes.
/// Supports the double-entry bookkeeping requirement of the platform.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Represents incoming funds from a client, typically via an Invoice payment.
    /// </summary>
    ClientPayment = 1,

    /// <summary>
    /// Represents outgoing funds transferred to a vendor for completed work.
    /// </summary>
    VendorPayout = 2,

    /// <summary>
    /// Represents revenue retained by the platform as a service fee.
    /// Typically calculated as a percentage or fixed amount from the Client Payment.
    /// </summary>
    PlatformFee = 3,

    /// <summary>
    /// Represents funds returned to a client, usually due to project cancellation or dispute resolution.
    /// </summary>
    Refund = 4
}