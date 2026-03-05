namespace EnterpriseMediator.Domain.Financials.Enums
{
    /// <summary>
    /// Classification of financial transactions within the system.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Incoming funds from a Client (e.g., Invoice payment).
        /// </summary>
        Payment = 0,

        /// <summary>
        /// Outgoing funds to a Vendor.
        /// </summary>
        Payout = 1,

        /// <summary>
        /// Return of funds to a Client.
        /// </summary>
        Refund = 2
    }
}