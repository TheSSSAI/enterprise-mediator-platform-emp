using EnterpriseMediator.UserManagement.Domain.Aggregates.User;
using EnterpriseMediator.UserManagement.Domain.Enums;
using EnterpriseMediator.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseMediator.UserManagement.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the User entity mapping to the database.
    /// Implements rigorous constraints for data integrity and performance.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table Mapping
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(u => u.Id);

            // Properties Configuration
            builder.Property(u => u.Id)
                .ValueGeneratedNever()
                .IsRequired();

            // Email - Value Object or Strong Type logic handled via conversion or direct mapping
            // Assuming Email is stored as a string column for querying efficiency with a unique index
            builder.Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("Email");

            // Unique Index on Email to ensure identity uniqueness at the database level
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500); // Sufficient length for Argon2id hashes

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.UserType)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (UserType)Enum.Parse(typeof(UserType), v))
                .HasMaxLength(50);

            builder.Property(u => u.ProfileId)
                .IsRequired(false); // Optional, as internal users might not have a profile

            builder.Property(u => u.CreatedOn)
                .IsRequired();

            builder.Property(u => u.LastModifiedOn)
                .IsRequired(false);

            // Mapping for Address Value Object (Owned Type)
            // This flattens the Address properties into the Users table
            builder.OwnsOne(u => u.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street)
                    .HasMaxLength(200)
                    .HasColumnName("Address_Street")
                    .IsRequired(false);

                addressBuilder.Property(a => a.City)
                    .HasMaxLength(100)
                    .HasColumnName("Address_City")
                    .IsRequired(false);

                addressBuilder.Property(a => a.State)
                    .HasMaxLength(100)
                    .HasColumnName("Address_State")
                    .IsRequired(false);

                addressBuilder.Property(a => a.Country)
                    .HasMaxLength(100)
                    .HasColumnName("Address_Country")
                    .IsRequired(false);

                addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(20)
                    .HasColumnName("Address_ZipCode")
                    .IsRequired(false);
            });

            // Concurrency Token
            builder.Property(u => u.RowVersion)
                .IsRowVersion();

            // Ignore Domain Events as they are not persisted directly
            builder.Ignore(u => u.DomainEvents);
        }
    }
}