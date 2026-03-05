using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.Financial.Domain.Entities;

namespace EnterpriseMediator.Financial.Domain.Interfaces
{
    /// <summary>
    /// Represents the result of a payment link creation request.
    /// </summary>
    public record PaymentLinkResult
    {
        /// <summary>
        /// The unique identifier of the payment intent/session from the provider (e.g., Stripe PaymentIntentId).
        /// </summary>
        public string PaymentId { get; init; } = string.Empty;

        /// <summary>
        /// The URL where the user should be redirected to complete the payment.
        /// </summary>
        public string PaymentUrl { get; init; } = string.Empty;

        /// <summary>
        /// The status of the created link (e.g., "active").
        /// </summary>
        public string Status { get; init; } = string.Empty;

        /// <summary>
        /// The timestamp when the link expires.
        /// </summary>
        public DateTimeOffset ExpiresAt { get; init; }
    }

    /// <summary>
    /// Defines the contract for external payment gateway interactions (e.g., Stripe).
    /// This abstraction allows the domain layer to request payment processing without
    /// depending on specific infrastructure SDKs.
    /// </summary>
    public interface IPaymentGateway
    {
        /// <summary>
        /// Creates a secure payment link or session for the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice entity containing amount and currency details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result containing the payment URL and tracking ID.</returns>
        /// <exception cref="ArgumentNullException">Thrown when invoice is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the invoice is in an invalid state for payment.</exception>
        Task<PaymentLinkResult> CreatePaymentLinkAsync(Invoice invoice, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifies the status of a payment with the provider.
        /// </summary>
        /// <param name="paymentId">The external payment ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The current status string from the provider.</returns>
        Task<string> GetPaymentStatusAsync(string paymentId, CancellationToken cancellationToken = default);
    }
}