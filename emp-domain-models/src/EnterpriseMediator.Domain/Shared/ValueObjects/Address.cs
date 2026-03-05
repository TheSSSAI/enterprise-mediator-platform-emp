using System.Collections.Generic;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;

namespace EnterpriseMediator.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a physical postal address.
    /// </summary>
    public class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string state, string country, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new BusinessRuleValidationException("Street is required.");
            if (string.IsNullOrWhiteSpace(city))
                throw new BusinessRuleValidationException("City is required.");
            if (string.IsNullOrWhiteSpace(country))
                throw new BusinessRuleValidationException("Country is required.");
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new BusinessRuleValidationException("ZipCode is required.");

            Street = street;
            City = city;
            State = state ?? string.Empty; // State might be optional in some countries
            Country = country;
            ZipCode = zipCode;
        }

        public static Address Create(string street, string city, string state, string country, string zipCode)
        {
            return new Address(street, city, state, country, zipCode);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street.ToLowerInvariant();
            yield return City.ToLowerInvariant();
            yield return State.ToLowerInvariant();
            yield return Country.ToLowerInvariant();
            yield return ZipCode.ToLowerInvariant();
        }

        public override string ToString()
        {
            var stateStr = string.IsNullOrEmpty(State) ? "" : $", {State}";
            return $"{Street}, {City}{stateStr}, {Country} {ZipCode}";
        }
    }
}