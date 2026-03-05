using System.Collections.Generic;
using System.Text.RegularExpressions;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;

namespace EnterpriseMediator.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a validated phone number.
    /// </summary>
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

        // Basic validation: allows +, -, space, brackets, and digits. 
        // Min 7 digits, Max 15 digits (E.164 standard is max 15).
        // This is a permissive check; strict validation often requires libphonenumber.
        private static readonly Regex PhoneRegex = new(
            @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$",
            RegexOptions.Compiled);

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessRuleValidationException("Phone number cannot be empty.");

            // Basic cleanup before validation check could be done, 
            // but strict VO usually validates raw input or expects cleaner input.
            // We will trim whitespace.
            var normalized = value.Trim();

            // Simple length check for sanity
            var digitsOnly = Regex.Replace(normalized, @"[^\d]", "");
            if (digitsOnly.Length < 7 || digitsOnly.Length > 15)
                throw new BusinessRuleValidationException($"Phone number must contain between 7 and 15 digits. Input: {value}");

            Value = normalized;
        }

        public static PhoneNumber Create(string value) => new(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Compare based on digits only to treat (555) 123-4567 same as 5551234567
            yield return Regex.Replace(Value, @"[^\d]", "");
        }

        public override string ToString() => Value;
    }
}