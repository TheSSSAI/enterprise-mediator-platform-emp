namespace EnterpriseMediator.Domain.Financials.Enums
{
    /// <summary>
    /// Defines the states of a Vendor Payout transaction.
    /// </summary>
    public enum PayoutStatus
    {
        /// <summary>
        /// Payout initiated but requires Finance Manager approval.
        /// </summary>
        PendingApproval = 0,

        /// <summary>
        /// Approved and currently being processed by the payment gateway.
        /// </summary>
        Processing = 1,

        /// <summary>
        /// Funds successfully sent to the vendor.
        /// </summary>
        Sent = 2,

        /// <summary>
        /// Payout transaction failed at the gateway or banking level.
        /// </summary>
        Failed = 3,

        /// <summary>
        /// Vendor has acknowledged receipt of funds.
        /// </summary>
        Completed = 4,

        /// <summary>
        /// Payout request rejected by Finance Manager.
        /// </summary>
        Rejected = 5
    }
}