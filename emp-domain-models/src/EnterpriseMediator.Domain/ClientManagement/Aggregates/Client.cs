using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.ClientManagement.Aggregates
{
    public class Client : AggregateRoot<ClientId>
    {
        private readonly List<ClientContact> _contacts = new();

        public string CompanyName { get; private set; }
        public Address Address { get; private set; }
        public Address? BillingAddress { get; private set; }
        public bool IsActive { get; private set; }
        
        public IReadOnlyCollection<ClientContact> Contacts => _contacts.AsReadOnly();

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        #pragma warning disable CS8618
        private Client() { }
        #pragma warning restore CS8618

        private Client(ClientId id, string companyName, Address address)
        {
            Id = id;
            CompanyName = !string.IsNullOrWhiteSpace(companyName) ? companyName : throw new BusinessRuleValidationException("Company Name is required.");
            Address = address ?? throw new ArgumentNullException(nameof(address));
            BillingAddress = address; // Default billing to main address
            IsActive = true;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Client Create(string companyName, Address address)
        {
            return new Client(new ClientId(Guid.NewGuid()), companyName, address);
        }

        public void UpdateProfile(string companyName, Address address, Address? billingAddress)
        {
            CompanyName = !string.IsNullOrWhiteSpace(companyName) ? companyName : throw new BusinessRuleValidationException("Company Name is required.");
            Address = address ?? throw new ArgumentNullException(nameof(address));
            BillingAddress = billingAddress;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void AddContact(ClientContact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));
            
            if (_contacts.Any(c => c.Email == contact.Email))
            {
                throw new BusinessRuleValidationException("Contact with this email already exists for this client.");
            }

            _contacts.Add(contact);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void RemoveContact(Guid contactId)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact != null)
            {
                _contacts.Remove(contact);
                UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        public void UpdateContact(Guid contactId, string name, Email email, PhoneNumber? phone)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact == null) throw new BusinessRuleValidationException("Contact not found.");

            // Check email uniqueness within client, excluding self
            if (_contacts.Any(c => c.Id != contactId && c.Email == email))
            {
                throw new BusinessRuleValidationException("Another contact with this email already exists.");
            }

            contact.Update(name, email, phone);
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Reactivate()
        {
            IsActive = true;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}