using System;
using System.Collections.Generic;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;

namespace EnterpriseMediator.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with a specific currency.
    /// Handles arithmetic operations safely and ensures currency compatibility.
    /// </summary>
    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public Money(decimal amount, Currency currency)
        {
            if (currency == null)
                throw new BusinessRuleValidationException("Money must have a defined currency.");

            // Domain rule: Money usually tracks to 2-4 decimal places.
            // We'll normalize to 2 for standard business logic, or rely on raw decimal precision.
            // Here we ensure we don't have dangling high-precision dust.
            Amount = Math.Round(amount, 2, MidpointRounding.ToEven);
            Currency = currency;
        }

        public static Money Zero(Currency currency) => new(0, currency);

        public static Money Of(decimal amount, string currencyCode) 
            => new(amount, Currency.FromCode(currencyCode));

        public Money Add(Money other)
        {
            if (other.Currency != Currency)
                throw new BusinessRuleValidationException($"Cannot add money with different currencies: {Currency.Code} and {other.Currency.Code}");

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            if (other.Currency != Currency)
                throw new BusinessRuleValidationException($"Cannot subtract money with different currencies: {Currency.Code} and {other.Currency.Code}");

            return new Money(Amount - other.Amount, Currency);
        }

        public Money Multiply(decimal multiplier)
        {
            return new Money(Amount * multiplier, Currency);
        }

        public Money Allocate(int ratio)
        {
            // Simple allocation wrapper
            return Multiply(ratio);
        }

        public static Money operator +(Money left, Money right) => left.Add(right);
        public static Money operator -(Money left, Money right) => left.Subtract(right);
        public static Money operator *(Money left, decimal right) => left.Multiply(right);

        public bool IsZero() => Amount == 0;
        public bool IsPositive() => Amount > 0;
        public bool IsNegative() => Amount < 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString() => $"{Currency.Symbol}{Amount:N2}";
    }
}