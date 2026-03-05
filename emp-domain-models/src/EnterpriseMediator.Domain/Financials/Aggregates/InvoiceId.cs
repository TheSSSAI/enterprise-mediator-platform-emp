using System;

namespace EnterpriseMediator.Domain.Financials.Aggregates
{
    /// <summary>
    /// Strongly typed identifier for an Invoice.
    /// </summary>
    /// <param name="Value">The underlying GUID value.</param>
    public readonly record struct InvoiceId(Guid Value) : IComparable<InvoiceId>
    {
        /// <summary>
        /// Creates a new unique InvoiceId.
        /// </summary>
        public static InvoiceId New() => new(Guid.NewGuid());

        /// <summary>
        /// Represents an empty InvoiceId.
        /// </summary>
        public static InvoiceId Empty => new(Guid.Empty);

        public int CompareTo(InvoiceId other) => Value.CompareTo(other.Value);

        public override string ToString() => Value.ToString();
    }
}