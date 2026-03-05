using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMediator.UserManagement.Domain.ValueObjects;
using EnterpriseMediator.UserManagement.Domain.Events;
using MediatR;

namespace EnterpriseMediator.UserManagement.Domain.Aggregates.Vendor
{
    /// <summary>
    /// Aggregate Root representing a Vendor organization.
    /// Manages skills, payment details, and vetting status.
    /// </summary>
    public class Vendor
    {
        public Guid Id { get; private set; }
        public string CompanyName { get; private set; }
        public Address Address { get; private set; }
        public PaymentInfo PaymentDetails { get; private set; }
        public string VettingStatus { get; private set; } // "Pending", "Active", "Rejected", "Deactivated"
        public string PrimaryContactEmail { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        private readonly List<VendorSkill> _skills = new();
        public IReadOnlyCollection<VendorSkill> Skills => _skills.AsReadOnly();

        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        // EF Core Constructor
        protected Vendor() { }

        private Vendor(string companyName, Address address, string primaryContactEmail, PaymentInfo paymentDetails)
        {
            Id = Guid.NewGuid();
            CompanyName = companyName;
            Address = address;
            PrimaryContactEmail = primaryContactEmail;
            PaymentDetails = paymentDetails;
            VettingStatus = "Pending";
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Vendor Create(string companyName, Address address, string primaryContactEmail, PaymentInfo paymentDetails = null)
        {
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("Company name is required", nameof(companyName));
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (string.IsNullOrWhiteSpace(primaryContactEmail)) throw new ArgumentException("Primary contact email is required", nameof(primaryContactEmail));

            var vendor = new Vendor(companyName.Trim(), address, primaryContactEmail.ToLowerInvariant().Trim(), paymentDetails);
            
            vendor.AddDomainEvent(new VendorCreatedEvent(vendor.Id, vendor.CompanyName, vendor.PrimaryContactEmail, vendor.CreatedAt));
            
            return vendor;
        }

        public void UpdateProfile(string companyName, Address address, string primaryContactEmail)
        {
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("Company name is required", nameof(companyName));
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (string.IsNullOrWhiteSpace(primaryContactEmail)) throw new ArgumentException("Primary contact email is required", nameof(primaryContactEmail));

            CompanyName = companyName.Trim();
            Address = address;
            PrimaryContactEmail = primaryContactEmail.ToLowerInvariant().Trim();
            UpdatedAt = DateTimeOffset.UtcNow;

            AddDomainEvent(new VendorProfileUpdated(Id, DateTimeOffset.UtcNow, skillsUpdated: false));
        }

        public void UpdatePaymentDetails(PaymentInfo paymentDetails)
        {
            // PaymentInfo is a value object and should be replaced entirely
            PaymentDetails = paymentDetails ?? throw new ArgumentNullException(nameof(paymentDetails));
            UpdatedAt = DateTimeOffset.UtcNow;
            
            // Note: We intentionally do not emit payment details in events for security
            AddDomainEvent(new VendorProfileUpdated(Id, DateTimeOffset.UtcNow, skillsUpdated: false));
        }

        /// <summary>
        /// Updates the vendor's skill set by syncing the current collection with the provided list of tags.
        /// Adds new skills and removes skills that are no longer present.
        /// </summary>
        public void UpdateSkills(IEnumerable<string> skillTags)
        {
            if (skillTags == null) throw new ArgumentNullException(nameof(skillTags));

            var distinctTags = skillTags.Where(t => !string.IsNullOrWhiteSpace(t))
                                      .Select(t => t.Trim())
                                      .Distinct(StringComparer.OrdinalIgnoreCase)
                                      .ToList();

            var existingTags = _skills.Select(s => s.Name).ToList();

            // Find skills to remove
            var skillsToRemove = _skills.Where(s => !distinctTags.Contains(s.Name, StringComparer.OrdinalIgnoreCase)).ToList();
            foreach (var skill in skillsToRemove)
            {
                _skills.Remove(skill);
            }

            // Find skills to add
            var tagsToAdd = distinctTags.Where(tag => !existingTags.Contains(tag, StringComparer.OrdinalIgnoreCase)).ToList();
            foreach (var tag in tagsToAdd)
            {
                _skills.Add(new VendorSkill(Id, tag));
            }

            if (skillsToRemove.Any() || tagsToAdd.Any())
            {
                UpdatedAt = DateTimeOffset.UtcNow;
                AddDomainEvent(new VendorProfileUpdated(Id, DateTimeOffset.UtcNow, skillsUpdated: true));
            }
        }

        public void ChangeVettingStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus)) throw new ArgumentException("New status cannot be empty", nameof(newStatus));
            if (VettingStatus == newStatus) return;

            string oldStatus = VettingStatus;
            VettingStatus = newStatus;
            UpdatedAt = DateTimeOffset.UtcNow;

            AddDomainEvent(new VendorVettingStatusChangedEvent(Id, oldStatus, newStatus, DateTimeOffset.UtcNow));
        }

        private void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}