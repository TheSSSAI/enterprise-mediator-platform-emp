using System;
using MediatR;

namespace EnterpriseMediator.UserManagement.Domain.Events
{
    /// <summary>
    /// Event raised when a vendor's vetting status changes (e.g., Pending -> Active).
    /// </summary>
    public record VendorVettingStatusChangedEvent : INotification
    {
        public Guid VendorId { get; }
        public string OldStatus { get; }
        public string NewStatus { get; }
        public DateTimeOffset ChangedAt { get; }

        public VendorVettingStatusChangedEvent(Guid vendorId, string oldStatus, string newStatus, DateTimeOffset changedAt)
        {
            if (vendorId == Guid.Empty) throw new ArgumentException("Vendor ID cannot be empty", nameof(vendorId));
            
            VendorId = vendorId;
            OldStatus = oldStatus ?? "Unknown";
            NewStatus = newStatus ?? throw new ArgumentNullException(nameof(newStatus));
            ChangedAt = changedAt;
        }
    }
}