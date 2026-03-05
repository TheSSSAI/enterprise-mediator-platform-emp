using System;
using MediatR;

namespace EnterpriseMediator.UserManagement.Domain.Events
{
    /// <summary>
    /// Event raised when a user's personally identifiable information (PII) has been anonymized
    /// for GDPR compliance or account deletion requests.
    /// </summary>
    public record UserAnonymizedEvent : INotification
    {
        public Guid UserId { get; }
        public DateTimeOffset AnonymizedAt { get; }

        public UserAnonymizedEvent(Guid userId, DateTimeOffset anonymizedAt)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty", nameof(userId));

            UserId = userId;
            AnonymizedAt = anonymizedAt;
        }
    }
}