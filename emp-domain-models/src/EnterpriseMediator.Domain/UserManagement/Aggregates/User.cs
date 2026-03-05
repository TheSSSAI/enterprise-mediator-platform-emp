using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Shared.ValueObjects;
using EnterpriseMediator.Domain.ClientManagement.Aggregates;
using EnterpriseMediator.Domain.VendorManagement.Aggregates;

namespace EnterpriseMediator.Domain.UserManagement.Aggregates
{
    public class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; } // Stored securely
        
        public RoleId RoleId { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsMfaEnabled { get; private set; }
        
        // Linkage to tenant contexts
        public ClientId? ClientId { get; private set; }
        public VendorId? VendorId { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? LastLoginAt { get; private set; }

        #pragma warning disable CS8618
        private User() { }
        #pragma warning restore CS8618

        private User(UserId id, string firstName, string lastName, Email email, RoleId roleId)
        {
            Id = id;
            FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new BusinessRuleValidationException("First name required.");
            LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new BusinessRuleValidationException("Last name required.");
            Email = email ?? throw new ArgumentNullException(nameof(email));
            RoleId = roleId ?? throw new ArgumentNullException(nameof(roleId));
            
            IsActive = true;
            IsMfaEnabled = false;
            CreatedAt = DateTimeOffset.UtcNow;
            
            // Password must be set via method
            PasswordHash = string.Empty; 
        }

        public static User CreateInternalUser(string firstName, string lastName, Email email, RoleId roleId)
        {
            return new User(new UserId(Guid.NewGuid()), firstName, lastName, email, roleId);
        }

        public static User CreateClientUser(string firstName, string lastName, Email email, RoleId roleId, ClientId clientId)
        {
            var user = new User(new UserId(Guid.NewGuid()), firstName, lastName, email, roleId);
            user.AssignToClient(clientId);
            return user;
        }

        public static User CreateVendorUser(string firstName, string lastName, Email email, RoleId roleId, VendorId vendorId)
        {
            var user = new User(new UserId(Guid.NewGuid()), firstName, lastName, email, roleId);
            user.AssignToVendor(vendorId);
            return user;
        }

        public void SetPassword(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new BusinessRuleValidationException("Invalid password hash.");
            PasswordHash = passwordHash;
        }

        public void UpdateProfile(string firstName, string lastName)
        {
            FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new BusinessRuleValidationException("First name required.");
            LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new BusinessRuleValidationException("Last name required.");
        }

        public void AssignToClient(ClientId clientId)
        {
            if (VendorId != null) throw new BusinessRuleValidationException("User cannot belong to both Client and Vendor.");
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
        }

        public void AssignToVendor(VendorId vendorId)
        {
            if (ClientId != null) throw new BusinessRuleValidationException("User cannot belong to both Client and Vendor.");
            VendorId = vendorId ?? throw new ArgumentNullException(nameof(vendorId));
        }

        public void ChangeRole(RoleId newRoleId)
        {
            RoleId = newRoleId ?? throw new ArgumentNullException(nameof(newRoleId));
        }

        public void EnableMfa()
        {
            IsMfaEnabled = true;
        }

        public void DisableMfa()
        {
            IsMfaEnabled = false;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void RecordLogin()
        {
            LastLoginAt = DateTimeOffset.UtcNow;
        }
    }
}