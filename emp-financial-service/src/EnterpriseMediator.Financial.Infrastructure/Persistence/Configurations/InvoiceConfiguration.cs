using EnterpriseMediator.Financial.Domain.Entities;
using EnterpriseMediator.Financial.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseMediator.Financial.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Entity Framework Core configuration for the Invoice aggregate root.
    /// Defines table mapping, column constraints, relationships, and value object handling.
    /// </summary>
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            // Table Mapping
            builder.ToTable("Invoices");

            // Primary Key
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .ValueGeneratedNever()
                .IsRequired();

            // Properties
            builder.Property(i => i.ProjectId)
                .IsRequired();

            builder.Property(i => i.ClientId)
                .IsRequired();

            // Enum Mapping - Stored as string for readability
            builder.Property(i => i.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), v))
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.StripePaymentIntentId)
                .HasMaxLength(255)
                .IsRequired(false); // Nullable until payment initiation

            // Value Object Mapping: Money (TotalAmount)
            // Using Complex Property (EF Core 8+) feature which delegates to MoneyConfiguration
            builder.ComplexProperty(i => i.TotalAmount);

            // Audit Properties (Assuming BaseEntity audit fields)
            builder.Property(i => i.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.CreatedOn)
                .IsRequired();

            builder.Property(i => i.LastModifiedBy)
                .HasMaxLength(100);

            // Indexes
            builder.HasIndex(i => i.ProjectId)
                .HasDatabaseName("IX_Invoices_ProjectId");

            builder.HasIndex(i => i.StripePaymentIntentId)
                .IsUnique()
                .HasFilter("\"StripePaymentIntentId\" IS NOT NULL")
                .HasDatabaseName("IX_Invoices_StripePaymentIntentId");

            // Relationships
            // Transactions relationship is typically defined on the Transaction side,
            // but we can define the navigation property here if it exists.
            builder.HasMany(i => i.Transactions)
                .WithOne()
                .HasForeignKey("InvoiceId") // Assuming Shadow FK or Property on Transaction
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}