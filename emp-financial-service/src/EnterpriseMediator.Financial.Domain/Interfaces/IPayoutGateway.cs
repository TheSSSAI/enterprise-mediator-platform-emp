using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.Financial.Domain.Entities;

namespace EnterpriseMediator.Financial.Domain.Interfaces
{
    /// <summary>
    /// Represents the result of a payout execution request.
    /// </summary>
    public record PayoutResult
    {
        /// <summary>
        /// The unique identifier of the transfer from the provider (e.g., Wise Transfer ID).
        /// </summary>
        public string TransferId { get; init; } = string.Empty;

        /// <summary>
        /// The current status of the transfer (e.g., "processing", "success").
        /// </summary>
        public string Status { get; init; } = string.Empty;

        /// <summary>
        /// The fee charged by the provider for this transfer.
        /// </summary>
        public decimal FeeAmount { get; init; }

        /// <summary>
        /// The currency of the fee.
        /// </summary>
        public string FeeCurrency { get; init; } = string.Empty;

        /// <summary>
        /// The estimated delivery time of the funds.
        /// </summary>
        public DateTimeOffset? EstimatedDelivery { get; init; }
    }

    /// <summary>
    /// Defines the contract for external payout gateway interactions (e.g., Wise).
    /// This abstraction ensures the domain logic for distributing funds is decoupled
    /// from the specific banking or transfer infrastructure.
    /// </summary>
    public interface IPayoutGateway
    {
        /// <summary>
        /// Executes a fund transfer to a vendor based on the payout entity details.
        /// </summary>
        /// <param name="payout">The payout entity containing vendor, amount, and currency details.</param>
        /// <param name="idempotencyKey">A unique key to ensure the payout is not processed twice.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result containing the transfer ID and status.</returns>
        /// <exception cref="ArgumentNullException">Thrown when payout is null.</exception>
        Task<PayoutResult> ExecutePayoutAsync(Payout payout, string idempotencyKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the current status of a specific transfer from the provider.
        /// </summary>
        /// <param name="transferId">The external transfer identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The status string.</returns>
        Task<string> GetPayoutStatusAsync(string transferId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Validates that the vendor's recipient account details are valid and ready to receive funds.
        /// </summary>
        /// <param name="vendorId">The identifier of the vendor.</param>
        /// <param name="currency">The currency to be transferred.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the recipient account is valid, otherwise false.</returns>
        Task<bool> ValidateRecipientAsync(Guid vendorId, string currency, CancellationToken cancellationToken = default);
    }
}