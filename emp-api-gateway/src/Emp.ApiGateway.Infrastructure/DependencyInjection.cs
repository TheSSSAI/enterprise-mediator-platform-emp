using Emp.ApiGateway.Application.Interfaces.Infrastructure;
using Emp.ApiGateway.Infrastructure.Configuration;
using Emp.ApiGateway.Infrastructure.Messaging;
using Emp.ApiGateway.Infrastructure.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Emp.ApiGateway.Infrastructure
{
    /// <summary>
    /// Provides extension methods for registering Infrastructure layer services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers Infrastructure services including HTTP clients, Messaging, and Configuration.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration manager.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Configuration Options
            services.Configure<ServiceUrls>(configuration.GetSection(ServiceUrls.SectionName));
            services.Configure<AwsCognitoSettings>(configuration.GetSection("AWS:Cognito"));

            // Register Typed HTTP Clients with Resilience Policies
            services.AddHttpClient<IProjectServiceClient, ProjectServiceClient>((serviceProvider, client) =>
            {
                var serviceUrls = serviceProvider.GetRequiredService<IOptions<ServiceUrls>>().Value;
                
                if (string.IsNullOrEmpty(serviceUrls.ProjectService))
                {
                    throw new InvalidOperationException("ProjectService URL is not configured.");
                }

                client.BaseAddress = new Uri(serviceUrls.ProjectService);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddStandardResilienceHandler(); // Applies retry, circuit breaker, and timeout policies

            services.AddHttpClient<IFinancialServiceClient, FinancialServiceClient>((serviceProvider, client) =>
            {
                var serviceUrls = serviceProvider.GetRequiredService<IOptions<ServiceUrls>>().Value;

                if (string.IsNullOrEmpty(serviceUrls.FinancialService))
                {
                    throw new InvalidOperationException("FinancialService URL is not configured.");
                }

                client.BaseAddress = new Uri(serviceUrls.FinancialService);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddStandardResilienceHandler();

            // Register MassTransit for Asynchronous Messaging
            services.AddMassTransit(x =>
            {
                // Default to RabbitMQ as the message broker
                x.UsingRabbitMq((context, cfg) =>
                {
                    // In a real environment, these would be pulled from Configuration
                    // cfg.Host("localhost", "/", h => {
                    //     h.Username("guest");
                    //     h.Password("guest");
                    // });
                    
                    cfg.ConfigureEndpoints(context);
                });
            });

            // Register Services
            services.AddScoped<MassTransitPublisher>();

            return services;
        }
    }
}