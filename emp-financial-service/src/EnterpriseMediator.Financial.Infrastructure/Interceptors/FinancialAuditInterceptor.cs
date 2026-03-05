using EnterpriseMediator.Financial.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EnterpriseMediator.Financial.Infrastructure.Interceptors
{
    /// <summary>
    /// Entity Framework Core Interceptor to automatically handle audit fields (CreatedOn, LastModifiedOn)
    /// before changes are saved to the database. This ensures SOC 2 compliance for data integrity.
    /// </summary>
    public class FinancialAuditInterceptor : SaveChangesInterceptor
    {
        private readonly DateTimeProvider _dateTimeProvider;

        public FinancialAuditInterceptor(DateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditFields(DbContext? context)
        {
            if (context == null) return;

            var utcNow = _dateTimeProvider.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    SetPropertyIfPresent(entry, "CreatedOn", utcNow);
                    SetPropertyIfPresent(entry, "LastModifiedOn", utcNow);
                }
                else if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    SetPropertyIfPresent(entry, "LastModifiedOn", utcNow);
                }
            }
        }

        /// <summary>
        /// Uses reflection to set the property value if it exists on the entity.
        /// This allows the interceptor to work with any entity type that follows the convention
        /// without enforcing a strict interface inheritance hierarchy at the framework level.
        /// </summary>
        private void SetPropertyIfPresent(EntityEntry entry, string propertyName, DateTime value)
        {
            var property = entry.Metadata.FindProperty(propertyName);
            if (property != null && property.ClrType == typeof(DateTime))
            {
                entry.Property(propertyName).CurrentValue = value;
            }
            else if (property != null && property.ClrType == typeof(DateTime?))
            {
                entry.Property(propertyName).CurrentValue = value;
            }
            else if (property != null && property.ClrType == typeof(DateTimeOffset))
            {
                entry.Property(propertyName).CurrentValue = (DateTimeOffset)value;
            }
            else if (property != null && property.ClrType == typeof(DateTimeOffset?))
            {
                entry.Property(propertyName).CurrentValue = (DateTimeOffset)value;
            }
        }
    }

    /// <summary>
    /// Extension methods to detect changes in owned entities (Value Objects) which should trigger 
    /// a modification timestamp update on the owner entity.
    /// </summary>
    public static class EntityEntryExtensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r => 
                r.TargetEntry != null && 
                r.TargetEntry.Metadata.IsOwned() && 
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}