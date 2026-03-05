using EnterpriseMediator.Financial.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseMediator.Financial.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Entity Framework Core 8 Complex Type configuration for the Money value object.
    /// Ensures consistent mapping of monetary values across all entities that use Money.
    /// </summary>
    public class MoneyConfiguration : IComplexTypeConfiguration<Money>
    {
        public void Configure(ComplexTypeBuilder<Money> builder)
        {
            // Amount Mapping
            // Uses decimal(18,2) standard for currency storage to prevent rounding errors
            builder.Property(m => m.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Currency Mapping
            // Uses ISO 4217 3-letter codes
            builder.Property(m => m.Currency)
                .HasMaxLength(3)
                .IsFixedLength()
                .IsRequired();
        }
    }
}