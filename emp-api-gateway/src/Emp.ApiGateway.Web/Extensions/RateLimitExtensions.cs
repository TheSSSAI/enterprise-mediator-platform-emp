using AspNetCoreRateLimit;

namespace Emp.ApiGateway.Web.Extensions
{
    /// <summary>
    /// Extension methods for configuring Rate Limiting middleware.
    /// Configures in-memory rate limiting based on IP addresses.
    /// </summary>
    public static class RateLimitExtensions
    {
        public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            // Needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            // Load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

            // Inject counter and rules stores
            services.AddInMemoryRateLimiting();

            // Configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }

        public static IApplicationBuilder UseCustomRateLimiting(this IApplicationBuilder app)
        {
            app.UseIpRateLimiting();
            return app;
        }
    }
}