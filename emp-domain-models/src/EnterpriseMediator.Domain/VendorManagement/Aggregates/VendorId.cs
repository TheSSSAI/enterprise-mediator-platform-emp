using System;

namespace EnterpriseMediator.Domain.VendorManagement.Aggregates
{
    /// <summary>
    /// Strongly typed identifier for a Vendor.
    /// </summary>
    /// <param name="Value">The underlying GUID value.</param>
    public readonly record struct VendorId(Guid Value) : IComparable<VendorId>
    {
        /// <summary>
        /// Creates a new unique VendorId.
        /// </summary>
        public static VendorId New() => new(Guid.NewGuid());

        /// <summary>
        /// Represents an empty VendorId.
        /// </summary>
        public static VendorId Empty => new(Guid.Empty);

        public int CompareTo(VendorId other) => Value.CompareTo(other.Value);

        public override string ToString() => Value.ToString();
    }
}