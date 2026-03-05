using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMediator.UserManagement.Domain.ValueObjects
{
    /// <summary>
    /// Represents a physical or mailing address.
    /// Implemented as an immutable record to provide structural equality semantics,
    /// ensuring that two addresses with the same values are considered equal.
    /// </summary>
    public record Address
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string Country { get; init; }

        // EF Core requires a parameterless constructor for materialization.
        // We mark it protected or private to enforce usage of the primary factory/constructor in domain logic.
        protected Address() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> record.
        /// Performs validation to ensure critical address components are present.
        /// </summary>
        /// <param name="street">The street address (e.g., "123 Innovation Way").</param>
        /// <param name="city">The city or locality.</param>
        /// <param name="state">The state, province, or region.</param>
        /// <param name="postalCode">The postal or zip code.</param>
        /// <param name="country">The country name or ISO code.</param>
        /// <exception cref="ArgumentException">Thrown when required fields are missing.</exception>
        public Address(string street, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.", nameof(street));
            
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.", nameof(city));
            
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty.", nameof(country));

            // State and PostalCode might be optional in some international contexts, 
            // but for business consistency we typically require them or a placeholder.
            // Here we allow them to be empty if the business rule permits, 
            // but for this implementation we'll sanitize nulls to empty strings.
            
            Street = street.Trim();
            City = city.Trim();
            State = state?.Trim() ?? string.Empty;
            PostalCode = postalCode?.Trim() ?? string.Empty;
            Country = country.Trim();
        }

        /// <summary>
        /// Creates a new Address instance using a static factory method for clearer intent.
        /// </summary>
        public static Address Create(string street, string city, string state, string postalCode, string country)
        {
            return new Address(street, city, state, postalCode, country);
        }

        /// <summary>
        /// Returns a formatted string representation of the address.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Street);
            sb.Append(", ");
            sb.Append(City);
            
            if (!string.IsNullOrWhiteSpace(State))
            {
                sb.Append(", ");
                sb.Append(State);
            }

            if (!string.IsNullOrWhiteSpace(PostalCode))
            {
                sb.Append(" ");
                sb.Append(PostalCode);
            }

            sb.Append(", ");
            sb.Append(Country);

            return sb.ToString();
        }
    }
}