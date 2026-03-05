using System.Reflection;
using EnterpriseMediator.UserManagement.Domain.Aggregates.Client;
using EnterpriseMediator.UserManagement.Domain.Aggregates.User;
using EnterpriseMediator.UserManagement.Domain.Aggregates.Vendor;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseMediator.UserManagement.Infrastructure.Persistence
{
    /// <summary>
    /// The Entity Framework Core database context for the User Management microservice.
    /// Manages persistence for Users, Vendors, and Clients.
    /// </summary>
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Represents the collection of Users in the system.
        /// </summary>
        public DbSet<User> Users => Set<User>();

        /// <summary>
        /// Represents the collection of Vendors in the system.
        /// </summary>
        public DbSet<Vendor> Vendors => Set<Vendor>();

        /// <summary>
        /// Represents the collection of Clients in the system.
        /// </summary>
        public DbSet<Client> Clients => Set<Client>();

        /// <summary>
        /// Configures the model using Fluent API configurations found in the current assembly.
        /// This method is called by the framework when the context is first created.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations defined in this assembly (e.g., UserConfiguration, VendorConfiguration)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Global configuration to ensure all DateTime properties are saved as UTC
            // This is critical for consistent timestamp handling across different timezones
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(
                            new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                                v => v.ToUniversalTime(),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }

            // Optional: Define a default schema for the microservice context if sharing a DB,
            // though typical microservices have dedicated DBs.
            // modelBuilder.HasDefaultSchema("user_management");
        }
    }
}