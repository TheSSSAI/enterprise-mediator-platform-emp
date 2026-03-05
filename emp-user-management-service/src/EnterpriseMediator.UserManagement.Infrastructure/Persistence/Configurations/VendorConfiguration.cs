using EnterpriseMediator.UserManagement.Domain.Aggregates.Vendor;
using EnterpriseMediator.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseMediator.UserManagement.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the Vendor entity mapping to the database.
    /// Includes handling for sensitive payment information and collection mapping for skills.
    /// </summary>
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            // Table Mapping
            builder.ToTable("Vendors");

            // Primary Key
            builder.HasKey(v => v.Id);

            // Properties Configuration
            builder.Property(v => v.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(v => v.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.PrimaryContactName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(v => v.PrimaryContactEmail)
                .HasMaxLength(255)
                .IsRequired();

            // Index on Company Name for faster lookups
            builder.HasIndex(v => v.CompanyName);

            builder.Property(v => v.VettingStatus)
                .IsRequired()
                .HasMaxLength(50); // Stored as string for readability (e.g., "Pending", "Active")

            builder.Property(v => v.CreatedOn)
                .IsRequired();

            builder.Property(v => v.LastModifiedOn)
                .IsRequired(false);

            // Address Value Object Mapping
            builder.OwnsOne(v => v.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street).HasColumnName("Address_Street").HasMaxLength(200).IsRequired();
                addressBuilder.Property(a => a.City).HasColumnName("Address_City").HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.State).HasColumnName("Address_State").HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.Country).HasColumnName("Address_Country").HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.ZipCode).HasColumnName("Address_ZipCode").HasMaxLength(20).IsRequired();
            });

            // PaymentInfo Value Object Mapping
            // Requirement US-028 mandates secure payment details. 
            // In a production environment, we would use a ValueConverter to encrypt these fields at rest.
            // Here we map them to columns, assuming the Application Layer or a ValueConverter handles the encryption logic.
            builder.OwnsOne(v => v.PaymentDetails, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.BankName)
                    .HasColumnName("Payment_BankName")
                    .HasMaxLength(150)
                    .IsRequired(false);

                paymentBuilder.Property(p => p.AccountNumber)
                    .HasColumnName("Payment_AccountNumber")
                    .HasMaxLength(100) // Accommodate encrypted string length
                    .IsRequired(false);

                paymentBuilder.Property(p => p.RoutingNumber)
                    .HasColumnName("Payment_RoutingNumber")
                    .HasMaxLength(100) // Accommodate encrypted string length
                    .IsRequired(false);

                paymentBuilder.Property(p => p.SwiftCode)
                    .HasColumnName("Payment_SwiftCode")
                    .HasMaxLength(50)
                    .IsRequired(false);
                
                paymentBuilder.Property(p => p.TaxId)
                    .HasColumnName("Payment_TaxId")
                    .HasMaxLength(100) // Encrypted
                    .IsRequired(false);
            });

            // VendorSkill Collection Mapping
            // Configured as a relationship to a separate table "VendorSkills"
            builder.OwnsMany(v => v.Skills, skillBuilder =>
            {
                skillBuilder.ToTable("VendorSkills");
                
                skillBuilder.WithOwner().HasForeignKey("VendorId");
                
                skillBuilder.Property(s => s.Id)
                    .ValueGeneratedNever(); // Assuming ID is generated in Domain

                skillBuilder.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                skillBuilder.Property(s => s.Level)
                    .IsRequired()
                    .HasMaxLength(50); // E.g., "Expert", "Intermediate"
            });

            // Metadata for concurrency
            builder.Property(v => v.RowVersion)
                .IsRowVersion();

            builder.Ignore(v => v.DomainEvents);
        }
    }
}