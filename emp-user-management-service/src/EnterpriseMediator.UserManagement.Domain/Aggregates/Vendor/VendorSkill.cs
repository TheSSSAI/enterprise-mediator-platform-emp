using System;

namespace EnterpriseMediator.UserManagement.Domain.Aggregates.Vendor
{
    /// <summary>
    /// Represents a specific skill or expertise tag associated with a vendor.
    /// Managed as part of the Vendor aggregate.
    /// </summary>
    public class VendorSkill
    {
        public Guid Id { get; private set; }
        public Guid VendorId { get; private set; }
        public string Name { get; private set; }

        // EF Core Constructor
        protected VendorSkill() { }

        public VendorSkill(Guid vendorId, string name)
        {
            if (vendorId == Guid.Empty)
                throw new ArgumentException("Vendor ID cannot be empty", nameof(vendorId));
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Skill name cannot be empty", nameof(name));

            Id = Guid.NewGuid();
            VendorId = vendorId;
            Name = name.Trim();
        }

        /// <summary>
        /// Updates the skill name.
        /// </summary>
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Skill name cannot be empty", nameof(newName));

            Name = newName.Trim();
        }
    }
}