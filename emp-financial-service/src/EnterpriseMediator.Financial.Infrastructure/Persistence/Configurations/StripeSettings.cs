using System.ComponentModel.DataAnnotations;

namespace EnterpriseMediator.Financial.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration settings for the Stripe payment gateway integration.
    /// Maps to the "Stripe" section in appsettings.json.
    /// </summary>
    public class StripeSettings
    {
        public const string SectionName = "Stripe";

        /// <summary>
        /// The secret API key for authentication with Stripe API.
        /// </summary>
        [Required(ErrorMessage = "Stripe API Key is required.")]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// The secret used to verify the signature of incoming webhooks from Stripe.
        /// </summary>
        [Required(ErrorMessage = "Stripe Webhook Secret is required.")]
        public string WebhookSecret { get; set; } = string.Empty;

        /// <summary>
        /// Configurable currency code for Stripe transactions (default: usd).
        /// </summary>
        public string DefaultCurrency { get; set; } = "usd";
    }
}