namespace EnterpriseMediator.Domain.Financials.Enums
{
    /// <summary>
    /// Defines the states of a Client Invoice.
    /// </summary>
    public enum InvoiceStatus
    {
        /// <summary>
        /// Invoice created but not yet sent to the client.
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Invoice sent to client, awaiting payment.
        /// </summary>
        Sent = 1,

        /// <summary>
        /// Payment successfully received.
        /// </summary>
        Paid = 2,

        /// <summary>
        /// Payment due date has passed without payment.
        /// </summary>
        Overdue = 3,

        /// <summary>
        /// Invoice voided or cancelled.
        /// </summary>
        Cancelled = 4
    }
}