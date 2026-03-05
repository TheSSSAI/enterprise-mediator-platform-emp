using System.ComponentModel.DataAnnotations;

namespace EnterpriseMediator.Financial.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration settings for the Wise (formerly TransferWise) payout gateway integration.
    /// Maps to the "Wise" section in appsettings.json.
    /// </summary>
    public class WiseSettings
    {
        public const string SectionName = "Wise";

        /// <summary>
        /// The API token for authentication with Wise API.
        /// </summary>
        [Required(ErrorMessage = "Wise API Token is required.")]
        public string ApiToken { get; set; } = string.Empty;

        /// <summary>
        /// The Business Profile ID associated with the Wise account.
        /// </summary>
        [Required(ErrorMessage = "Wise Profile ID is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Wise Profile ID must be a positive integer.")]
        public long ProfileId { get; set; }

        /// <summary>
        /// The base URL for the Wise API (allows switching between sandbox and production).
        /// </summary>
        public string BaseUrl { get; set; } = "https://api.sandbox.transferwise.tech/v1";
    }
}