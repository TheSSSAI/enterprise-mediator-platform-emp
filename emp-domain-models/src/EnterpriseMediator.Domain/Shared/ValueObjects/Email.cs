using System.Collections.Generic;
using System.Text.RegularExpressions;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;

namespace EnterpriseMediator.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a valid email address.
    /// </summary>
    public class Email : ValueObject
    {
        public string Value { get; }

        // Standard simplified regex for email validation
        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleValidationException("Email address cannot be empty.");

            var normalized = value.Trim();

            if (!EmailRegex.IsMatch(normalized))
                throw new BusinessRuleValidationException($"Invalid email format: {value}");

            Value = normalized;
        }

        public static Email Create(string value) => new(value);

        public static explicit operator string(Email email) => email.Value;
        public static explicit operator Email(string value) => new(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLowerInvariant();
        }

        public override string ToString() => Value;
    }
}