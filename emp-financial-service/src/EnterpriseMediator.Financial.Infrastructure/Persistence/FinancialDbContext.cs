using System.Reflection;
using EnterpriseMediator.Financial.Domain.Entities;
using EnterpriseMediator.Financial.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseMediator.Financial.Infrastructure.Persistence
{
    /// <summary>
    /// The Entity Framework Core Database Context for the Financial Microservice.
    /// Manages persistence for Invoices, Payouts, and Transactions.
    /// </summary>
    public class FinancialDbContext : DbContext
    {
        private readonly FinancialAuditInterceptor _auditInterceptor;

        public FinancialDbContext(
            DbContextOptions<FinancialDbContext> options,
            FinancialAuditInterceptor auditInterceptor) 
            : base(options)
        {
            _auditInterceptor = auditInterceptor ?? throw new ArgumentNullException(nameof(auditInterceptor));
        }

        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Payout> Payouts => Set<Payout>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Register the audit interceptor to automatically handle Created/Modified timestamps
            optionsBuilder.AddInterceptors(_auditInterceptor);
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the database schema for this microservice to avoid collisions
            modelBuilder.HasDefaultSchema("financial");

            // Apply all configurations from the current assembly (Level 5 configurations)
            // This includes InvoiceConfiguration, MoneyConfiguration, StripeSettings, etc.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}