using System;
using EnterpriseMediator.Core.SharedKernel.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace EnterpriseMediator.Core.SharedKernel.Extensions
{
    /// <summary>
    /// Extension methods for configuring Serilog across the enterprise services.
    /// Ensures consistent logging patterns, enrichment, and sinks.
    /// </summary>
    public static class LoggerConfigurationExtensions
    {
        public static void ConfigureSharedKernelLogging(
            this LoggerConfiguration loggerConfiguration,
            SerilogOptions options,
            IConfiguration configuration,
            string applicationName)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Base configuration
            loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", applicationName)
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production");

            // Console Logging (Standard Output)
            if (options.UseConsole)
            {
                loggerConfiguration.WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}");
            }

            // File Logging
            if (options.UseFile && !string.IsNullOrWhiteSpace(options.LogFilePath))
            {
                loggerConfiguration.WriteTo.File(
                    path: options.LogFilePath,
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Warning,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
            }

            // Seq Logging (Centralized Aggregation)
            if (options.UseSeq && !string.IsNullOrWhiteSpace(options.SeqUrl))
            {
                loggerConfiguration.WriteTo.Seq(options.SeqUrl);
            }

            // Elasticsearch Logging
            if (options.UseElasticsearch && !string.IsNullOrWhiteSpace(options.ElasticsearchUrl))
            {
                // Note: Actual Elastic sink requires Serilog.Sinks.Elasticsearch package.
                // Assuming package presence or placeholder for enterprise configuration.
                // loggerConfiguration.WriteTo.Elasticsearch(...)
            }
        }
    }
}