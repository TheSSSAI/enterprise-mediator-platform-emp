using System;
using MediatR;

namespace EnterpriseMediator.UserManagement.Domain.Events
{
    /// <summary>
    /// Event raised when a vendor updates their profile information.
    /// Used to synchronize data with other services (e.g., Project Matching service for skills).
    /// </summary>
    public record VendorProfileUpdated : INotification
    {
        public Guid VendorId { get; }
        public DateTimeOffset UpdatedAt { get; }
        public bool SkillsUpdated { get; }

        public VendorProfileUpdated(Guid vendorId, DateTimeOffset updatedAt, bool skillsUpdated = false)
        {
            if (vendorId == Guid.Empty) throw new ArgumentException("Vendor ID cannot be empty", nameof(vendorId));

            VendorId = vendorId;
            UpdatedAt = updatedAt;
            SkillsUpdated = skillsUpdated;
        }
    }
}