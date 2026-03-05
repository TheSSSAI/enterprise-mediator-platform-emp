namespace Emp.ApiGateway.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration settings for downstream microservice endpoints.
    /// This class is bound to the "ServiceUrls" section in appsettings.json.
    /// </summary>
    public class ServiceUrls
    {
        public const string SectionName = "ServiceUrls";

        /// <summary>
        /// Base URL for the Project Microservice.
        /// </summary>
        public string ProjectService { get; set; } = string.Empty;

        /// <summary>
        /// Base URL for the Financial Microservice.
        /// </summary>
        public string FinancialService { get; set; } = string.Empty;

        /// <summary>
        /// Base URL for the User/Identity Microservice.
        /// </summary>
        public string UserService { get; set; } = string.Empty;
    }
}