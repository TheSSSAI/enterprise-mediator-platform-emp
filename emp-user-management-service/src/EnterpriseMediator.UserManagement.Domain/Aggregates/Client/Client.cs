using System;
using System.Collections.Generic;
using EnterpriseMediator.UserManagement.Domain.ValueObjects;
using MediatR;

namespace EnterpriseMediator.UserManagement.Domain.Aggregates.Client
{
    /// <summary>
    /// Aggregate Root representing a Client organization.
    /// </summary>
    public class Client
    {
        public Guid Id { get; private set; }
        public string CompanyName { get; private set; }
        public Address CompanyAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public string PrimaryContactEmail { get; private set; }
        public string Status { get; private set; } // e.g., "Active", "Inactive"
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        // EF Core Constructor
        protected Client() { }

        private Client(string companyName, Address companyAddress, Address billingAddress, string primaryContactEmail)
        {
            Id = Guid.NewGuid();
            CompanyName = companyName;
            CompanyAddress = companyAddress;
            BillingAddress = billingAddress;
            PrimaryContactEmail = primaryContactEmail;
            Status = "Active";
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Client Create(string companyName, Address companyAddress, Address billingAddress, string primaryContactEmail)
        {
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("Company name is required", nameof(companyName));
            if (companyAddress == null) throw new ArgumentNullException(nameof(companyAddress));
            if (billingAddress == null) throw new ArgumentNullException(nameof(billingAddress));
            if (string.IsNullOrWhiteSpace(primaryContactEmail)) throw new ArgumentException("Primary contact email is required", nameof(primaryContactEmail));

            return new Client(companyName.Trim(), companyAddress, billingAddress, primaryContactEmail.ToLowerInvariant().Trim());
        }

        public void UpdateDetails(string companyName, string primaryContactEmail)
        {
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("Company name is required", nameof(companyName));
            if (string.IsNullOrWhiteSpace(primaryContactEmail)) throw new ArgumentException("Primary contact email is required", nameof(primaryContactEmail));

            CompanyName = companyName.Trim();
            PrimaryContactEmail = primaryContactEmail.ToLowerInvariant().Trim();
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateAddresses(Address companyAddress, Address billingAddress)
        {
            CompanyAddress = companyAddress ?? throw new ArgumentNullException(nameof(companyAddress));
            BillingAddress = billingAddress ?? throw new ArgumentNullException(nameof(billingAddress));
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Deactivate()
        {
            if (Status != "Inactive")
            {
                Status = "Inactive";
                UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        public void Activate()
        {
            if (Status != "Active")
            {
                Status = "Active";
                UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}