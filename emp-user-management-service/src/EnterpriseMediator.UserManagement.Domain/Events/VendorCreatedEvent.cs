using System;
using MediatR;

namespace EnterpriseMediator.UserManagement.Domain.Events
{
    /// <summary>
    /// Event raised when a new vendor profile is successfully created.
    /// Triggers downstream processes such as initial vetting workflows or welcome notifications.
    /// </summary>
    public record VendorCreatedEvent : INotification
    {
        public Guid VendorId { get; }
        public string CompanyName { get; }
        public string PrimaryContactEmail { get; }
        public DateTimeOffset CreatedAt { get; }

        public VendorCreatedEvent(Guid vendorId, string companyName, string primaryContactEmail, DateTimeOffset createdAt)
        {
            if (vendorId == Guid.Empty) throw new ArgumentException("Vendor ID cannot be empty", nameof(vendorId));
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("Company name cannot be empty", nameof(companyName));
            if (string.IsNullOrWhiteSpace(primaryContactEmail)) throw new ArgumentException("Primary contact email cannot be empty", nameof(primaryContactEmail));

            VendorId = vendorId;
            CompanyName = companyName;
            PrimaryContactEmail = primaryContactEmail;
            CreatedAt = createdAt;
        }
    }
}