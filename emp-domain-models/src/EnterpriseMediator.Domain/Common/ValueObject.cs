using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseMediator.Domain.Common
{
    /// <summary>
    /// Base class for Value Objects.
    /// Value Objects are objects that matter only as the combination of their attributes.
    /// Two Value Objects with the same values are considered equal.
    /// </summary>
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// Returns the components that determine the equality of this Value Object.
        /// </summary>
        /// <returns>An enumeration of objects to compare.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }
    }
}