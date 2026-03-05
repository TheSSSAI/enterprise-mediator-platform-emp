using System;
using System.Collections.Generic;
using EnterpriseMediator.UserManagement.Domain.Enums;
using EnterpriseMediator.UserManagement.Domain.Events;
using MediatR;

namespace EnterpriseMediator.UserManagement.Domain.Aggregates.User
{
    /// <summary>
    /// Aggregate Root representing a system user (Internal, Client Contact, or Vendor Contact).
    /// Responsible for authentication identity, profile status, and GDPR compliance.
    /// </summary>
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PasswordHash { get; private set; }
        public UserType Type { get; private set; }
        public bool IsActive { get; private set; }
        public Guid? ProfileId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? LastLoginAt { get; private set; }

        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        // EF Core Constructor
        protected User() { }

        private User(string email, string firstName, string lastName, string passwordHash, UserType type, Guid? profileId)
        {
            Id = Guid.NewGuid();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            Type = type;
            ProfileId = profileId;
            IsActive = true; // Default to active upon creation
            CreatedAt = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Factory method to register a new user.
        /// </summary>
        public static User Create(string email, string firstName, string lastName, string passwordHash, UserType type, Guid? profileId = null)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required", nameof(lastName));
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("Password hash is required", nameof(passwordHash));

            return new User(email.ToLowerInvariant().Trim(), firstName.Trim(), lastName.Trim(), passwordHash, type, profileId);
        }

        /// <summary>
        /// Updates the user's personal profile information.
        /// </summary>
        public void UpdateProfile(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required", nameof(lastName));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        /// <summary>
        /// Updates the user's password hash.
        /// </summary>
        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash)) throw new ArgumentException("New password hash cannot be empty", nameof(newPasswordHash));
            PasswordHash = newPasswordHash;
        }

        /// <summary>
        /// Deactivates the user account, preventing login.
        /// </summary>
        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
            }
        }

        /// <summary>
        /// Reactivates the user account.
        /// </summary>
        public void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
            }
        }

        /// <summary>
        /// Records a successful login.
        /// </summary>
        public void RecordLogin()
        {
            LastLoginAt = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Anonymizes the user data for GDPR compliance (Right to be Forgotten).
        /// This is an irreversible operation.
        /// </summary>
        public void Anonymize()
        {
            if (!IsActive)
            {
                // Ensure user is deactivated first or deactivate now
                IsActive = false;
            }

            // Replace PII with anonymized data
            // Maintain the ID for referential integrity in logs, but remove identity
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            Email = $"anonymized_{Id}_{timestamp}@deleted.local";
            FirstName = "Anonymized";
            LastName = "User";
            PasswordHash = string.Empty; // Remove credentials

            AddDomainEvent(new UserAnonymizedEvent(Id, DateTimeOffset.UtcNow));
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