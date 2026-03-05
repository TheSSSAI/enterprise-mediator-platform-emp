using System;

namespace EnterpriseMediator.UserManagement.Domain.ValueObjects
{
    /// <summary>
    /// Represents sensitive financial payment details for a vendor.
    /// This value object encapsulates banking or payment provider information.
    /// Note: Actual encryption at rest is handled by the Infrastructure layer (EF Core Value Converters),
    /// but this domain object enforces valid structure and provides safe masking capabilities.
    /// </summary>
    public record PaymentInfo
    {
        /// <summary>
        /// The name of the payment provider or bank (e.g., "Wise", "Chase", "Stripe Connect").
        /// </summary>
        public string ProviderName { get; init; }

        /// <summary>
        /// The primary account identifier (e.g., IBAN, Account Number, or Wallet ID).
        /// This is sensitive data.
        /// </summary>
        public string AccountIdentifier { get; init; }

        /// <summary>
        /// The routing identifier (e.g., SWIFT/BIC, Routing Number, Sort Code).
        /// Can be null if not applicable for the specific provider type.
        /// </summary>
        public string? RoutingIdentifier { get; init; }

        /// <summary>
        /// The ISO 4217 currency code expected for payments (e.g., "USD", "EUR").
        /// </summary>
        public string Currency { get; init; }

        // EF Core constructor
        protected PaymentInfo() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PaymentInfo"/>.
        /// </summary>
        /// <param name="providerName">Name of the bank or provider.</param>
        /// <param name="accountIdentifier">The account number or ID.</param>
        /// <param name="currency">The target currency.</param>
        /// <param name="routingIdentifier">Optional routing number/SWIFT code.</param>
        public PaymentInfo(string providerName, string accountIdentifier, string currency, string? routingIdentifier = null)
        {
            if (string.IsNullOrWhiteSpace(providerName))
                throw new ArgumentException("Payment provider name is required.", nameof(providerName));

            if (string.IsNullOrWhiteSpace(accountIdentifier))
                throw new ArgumentException("Account identifier is required.", nameof(accountIdentifier));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency code is required.", nameof(currency));

            if (currency.Length != 3)
                throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currency));

            ProviderName = providerName.Trim();
            AccountIdentifier = accountIdentifier.Trim();
            Currency = currency.Trim().ToUpperInvariant();
            RoutingIdentifier = routingIdentifier?.Trim();
        }

        /// <summary>
        /// Creates a new PaymentInfo instance.
        /// </summary>
        public static PaymentInfo Create(string providerName, string accountIdentifier, string currency, string? routingIdentifier = null)
        {
            return new PaymentInfo(providerName, accountIdentifier, currency, routingIdentifier);
        }

        /// <summary>
        /// Returns a secure, masked version of the payment details suitable for UI display or logging.
        /// Example: "Wise - ************5678 (USD)"
        /// </summary>
        public string GetMaskedDetails()
        {
            // Determine how many characters to show at the end. 
            // If the identifier is very short (unlikely for valid accounts), show less.
            const int visibleChars = 4;
            
            string maskedAccount;
            if (AccountIdentifier.Length <= visibleChars)
            {
                // Edge case for very short identifiers: mask all but last char or full mask
                maskedAccount = new string('*', Math.Max(4, AccountIdentifier.Length));
            }
            else
            {
                var prefixLength = AccountIdentifier.Length - visibleChars;
                maskedAccount = new string('*', prefixLength) + AccountIdentifier.Substring(prefixLength);
            }

            return $"{ProviderName} - {maskedAccount} ({Currency})";
        }

        /// <summary>
        /// Returns the masked representation when ToString is called to prevent accidental logging of cleartext sensitive data.
        /// </summary>
        public override string ToString()
        {
            return GetMaskedDetails();
        }
    }
}