using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.VendorManagement.Aggregates
{
    public class Vendor : AggregateRoot<VendorId>
    {
        private readonly List<VendorSkill> _skills = new();

        public string CompanyName { get; private set; }
        public Address Address { get; private set; }
        public string PrimaryContactName { get; private set; }
        public Email PrimaryContactEmail { get; private set; }
        public PhoneNumber? PrimaryContactPhone { get; private set; }
        
        // Status tracking
        public bool IsActive { get; private set; }
        public bool IsVetted { get; private set; }
        public bool IsDeactivated { get; private set; }

        public IReadOnlyCollection<VendorSkill> Skills => _skills.AsReadOnly();

        // Payment info is typically sensitive, possibly stored encrypted or handled via reference. 
        // For Domain modeling, we might keep a reference ID to a secure vault or basic unmasked info if allowed.
        // Assuming string for simplicity or ValueObject if complex.
        public string? PaymentDetailsReference { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        // EF Core Constructor
        #pragma warning disable CS8618
        private Vendor() { }
        #pragma warning restore CS8618

        private Vendor(VendorId id, string companyName, Address address, string primaryContactName, Email primaryContactEmail, PhoneNumber? primaryContactPhone)
        {
            Id = id;
            CompanyName = !string.IsNullOrWhiteSpace(companyName) ? companyName : throw new BusinessRuleValidationException("Company Name is required.");
            Address = address ?? throw new ArgumentNullException(nameof(address));
            PrimaryContactName = !string.IsNullOrWhiteSpace(primaryContactName) ? primaryContactName : throw new BusinessRuleValidationException("Primary Contact Name is required.");
            PrimaryContactEmail = primaryContactEmail ?? throw new ArgumentNullException(nameof(primaryContactEmail));
            PrimaryContactPhone = primaryContactPhone;
            
            // Default state
            IsActive = false; // Pending Vetting
            IsVetted = false;
            IsDeactivated = false;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Vendor Create(string companyName, Address address, string contactName, Email contactEmail, PhoneNumber? contactPhone = null)
        {
            return new Vendor(new VendorId(Guid.NewGuid()), companyName, address, contactName, contactEmail, contactPhone);
        }

        public void UpdateProfile(string companyName, Address address, string contactName, PhoneNumber? contactPhone)
        {
            CompanyName = !string.IsNullOrWhiteSpace(companyName) ? companyName : throw new BusinessRuleValidationException("Company Name is required.");
            Address = address ?? throw new ArgumentNullException(nameof(address));
            PrimaryContactName = !string.IsNullOrWhiteSpace(contactName) ? contactName : throw new BusinessRuleValidationException("Primary Contact Name is required.");
            PrimaryContactPhone = contactPhone;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void AddSkill(string skillName, int yearsExperience)
        {
            if (_skills.Any(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase)))
            {
                return; // Idempotent
            }
            
            _skills.Add(new VendorSkill(skillName, yearsExperience));
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void RemoveSkill(string skillName)
        {
            var skill = _skills.FirstOrDefault(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
            if (skill != null)
            {
                _skills.Remove(skill);
                UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        public void SetPaymentDetails(string paymentReference)
        {
            if (string.IsNullOrWhiteSpace(paymentReference)) throw new BusinessRuleValidationException("Payment details cannot be empty.");
            PaymentDetailsReference = paymentReference;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void VetAndActivate()
        {
            if (IsDeactivated) throw new BusinessRuleValidationException("Cannot vet a deactivated vendor.");
            IsVetted = true;
            IsActive = true;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            IsDeactivated = true;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Reactivate()
        {
            if (!IsVetted) throw new BusinessRuleValidationException("Cannot reactivate an unvetted vendor.");
            IsDeactivated = false;
            IsActive = true;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}