using System.Reflection;
using EnterpriseMediator.Core.SharedKernel.Abstractions;
using EnterpriseMediator.Core.SharedKernel.Behaviors;
using EnterpriseMediator.Core.SharedKernel.Common;
using EnterpriseMediator.Core.SharedKernel.Configuration;
using EnterpriseMediator.Core.SharedKernel.Implementations.Data;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EnterpriseMediator.Core.SharedKernel.Extensions;

/// <summary>
/// Provides extension methods for IServiceCollection to register Shared Kernel components.
/// This acts as the Composition Root helper for the shared infrastructure layer.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all core Shared Kernel services, behaviors, and configurations into the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="assembliesToScan">Optional list of assemblies to scan for Validators or other auto-registered types. If null, no assembly scanning is performed for Validators.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSharedKernel(
        this IServiceCollection services, 
        IConfiguration configuration,
        params Assembly[] assembliesToScan)
    {
        // 1. Register Configuration Options
        // We bind the configuration sections to the strongly-typed options classes.
        // This allows injecting IOptions<T> into services.
        services.Configure<SharedKernelOptions>(configuration.GetSection(nameof(SharedKernelOptions)));
        services.Configure<SerilogOptions>(configuration.GetSection($"{nameof(SharedKernelOptions)}:{nameof(SerilogOptions)}"));
        services.Configure<ResiliencyOptions>(configuration.GetSection($"{nameof(SharedKernelOptions)}:{nameof(ResiliencyOptions)}"));

        // 2. Register Core Services
        // IDateTimeProvider is used for testable date/time generation.
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        // 3. Register Data Access Abstractions
        // Register the open generic definitions for Repositories.
        // This relies on the consumer registering a DbContext that EfRepository can resolve.
        services.TryAddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.TryAddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        // 4. Register MediatR Pipeline Behaviors
        // The order of registration determines the order of execution in the pipeline.
        // Order: Logging (Outer) -> Performance -> Validation (Inner) -> Handler
        
        // Logs the request entry, exit, and any exceptions.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        
        // Measures execution time and logs warnings if thresholds are exceeded.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        
        // Validates the request using FluentValidation before it reaches the handler.
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // 5. Register FluentValidation
        // If assemblies are provided, we scan them for IValidator<T> implementations.
        if (assembliesToScan != null && assembliesToScan.Length > 0)
        {
            services.AddValidatorsFromAssemblies(assembliesToScan, ServiceLifetime.Transient);
        }

        // 6. Register Resiliency Policies
        // Utilizes the extension method from Level 1 to configure Polly policies.
        // This ensures consistent retry and circuit breaker logic across services.
        services.AddResiliencyPolicies(configuration);

        return services;
    }
}