using System.Collections.Generic;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;

namespace EnterpriseMediator.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a currency value object.
    /// </summary>
    public class Currency : ValueObject
    {
        public string Code { get; }
        public string Symbol { get; }

        // Common currencies defined as static properties for ease of use
        public static Currency USD => new("USD", "$");
        public static Currency EUR => new("EUR", "€");
        public static Currency GBP => new("GBP", "£");
        public static Currency CAD => new("CAD", "C$");
        public static Currency AUD => new("AUD", "A$");
        public static Currency JPY => new("JPY", "¥");

        private Currency(string code, string symbol)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new BusinessRuleValidationException("Currency code cannot be empty.");
            
            if (code.Length != 3)
                throw new BusinessRuleValidationException("Currency code must be exactly 3 characters.");

            Code = code.ToUpperInvariant();
            Symbol = symbol;
        }

        public static Currency FromCode(string code)
        {
            // In a real scenario, this might validate against a standard ISO list.
            // For now, we allow creation if format is valid, defaulting symbol to code if unknown.
            return new Currency(code, code);
        }

        public static Currency Create(string code, string symbol)
        {
            return new Currency(code, symbol);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public override string ToString() => Code;
    }
}