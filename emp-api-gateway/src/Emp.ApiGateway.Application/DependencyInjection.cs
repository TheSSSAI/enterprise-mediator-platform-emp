using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Emp.ApiGateway.Application
{
    /// <summary>
    /// Provides extension methods for registering Application layer services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the Application layer services including MediatR handlers and AutoMapper profiles.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Register MediatR for Command and Query handling
            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(assembly);
                // Future: Add pipeline behaviors (Validation, Logging) here
            });

            // Register AutoMapper for DTO mapping
            services.AddAutoMapper(assembly);

            return services;
        }
    }
}