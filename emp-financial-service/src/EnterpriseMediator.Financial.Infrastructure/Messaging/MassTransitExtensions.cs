using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseMediator.Financial.Infrastructure.Messaging
{
    /// <summary>
    /// Extension methods for configuring MassTransit with RabbitMQ in the Dependency Injection container.
    /// Handles consumer registration, bus configuration, and retry policies.
    /// </summary>
    public static class MassTransitExtensions
    {
        /// <summary>
        /// Adds MassTransit services to the service collection, configuring RabbitMQ as the transport.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="applicationAssembly">The assembly containing Application layer consumers.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddFinancialMessaging(
            this IServiceCollection services, 
            IConfiguration configuration,
            Assembly applicationAssembly)
        {
            services.AddMassTransit(busConfigurator =>
            {
                // Set Kebab Case Endpoint Naming (standard convention)
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                // Automatically discover and register all consumers in the Application assembly
                // This adheres to Clean Architecture by keeping Consumer logic in Application layer
                busConfigurator.AddConsumers(applicationAssembly);

                // Configure RabbitMQ Transport
                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqHost = configuration["RabbitMq:Host"] ?? "localhost";
                    var rabbitMqUser = configuration["RabbitMq:Username"] ?? "guest";
                    var rabbitMqPass = configuration["RabbitMq:Password"] ?? "guest";
                    var virtualHost = configuration["RabbitMq:VirtualHost"] ?? "/";

                    cfg.Host(rabbitMqHost, virtualHost, h =>
                    {
                        h.Username(rabbitMqUser);
                        h.Password(rabbitMqPass);
                    });

                    // Global Retry Policy
                    // Retries transient failures to ensure resilience
                    cfg.UseMessageRetry(r => r.Exponential(
                        retryLimit: 5, 
                        minInterval: TimeSpan.FromSeconds(1), 
                        maxInterval: TimeSpan.FromSeconds(30), 
                        delta: TimeSpan.FromSeconds(5)));

                    // Configure endpoints for all registered consumers
                    // This creates queues based on consumer names
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}