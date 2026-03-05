using EnterpriseMediator.Core.SharedKernel.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace EnterpriseMediator.Core.SharedKernel.Extensions;

/// <summary>
/// Extension methods for WebApplicationBuilder to configure shared infrastructure
/// during the application bootstrapping phase.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the application to use Serilog as the logging provider with standard enterprise settings.
    /// Reads configuration from the "Serilog" section of the configuration provider (appsettings.json).
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <param name="applicationName">Optional application name to enrich logs with. If null, tries to read from configuration or environment.</param>
    /// <returns>The builder instance for chaining.</returns>
    public static WebApplicationBuilder ConfigureSharedKernelLogging(
        this WebApplicationBuilder builder, 
        string? applicationName = null)
    {
        // 1. Clear default providers to prevent duplicate logging and noise
        builder.Logging.ClearProviders();

        // 2. Load Serilog Configuration options
        var serilogOptions = new SerilogOptions();
        var configSection = builder.Configuration.GetSection(SharedKernelOptions.SerilogSectionName);
        
        if (configSection.Exists())
        {
            configSection.Bind(serilogOptions);
        }

        // 3. Configure Serilog using the centralized extension from Level 1
        // If applicationName is passed explicitly, use it; otherwise fallback to options or default
        string appName = applicationName ?? serilogOptions.ApplicationName ?? builder.Environment.ApplicationName;

        builder.Host.UseSerilog((context, services, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .ConfigureBaseLogging(appName, builder.Environment.EnvironmentName, serilogOptions);
        });

        return builder;
    }

    /// <summary>
    /// Configures standard Kestrel and IIS integration settings if needed.
    /// Can be expanded to include default security headers or limits.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <returns>The builder instance for chaining.</returns>
    public static WebApplicationBuilder ConfigureDefaultWebSettings(this WebApplicationBuilder builder)
    {
        // Enforce Kestrel limitations for security resilience
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
            // Additional default limits can be set here (e.g. MaxRequestBodySize)
            // based on generic enterprise policies
        });

        return builder;
    }
}