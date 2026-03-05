namespace EnterpriseMediator.Financial.Domain.ValueObjects;

/// <summary>
/// Value Object representing a monetary value.
/// Encapsulates an amount and a currency, ensuring arithmetic operations are only performed on matching currencies.
/// Immutable and thread-safe.
/// </summary>
public record Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    /// <summary>
    /// Creates a new Money instance.
    /// </summary>
    /// <param name="amount">The numeric amount.</param>
    /// <param name="currency">The currency of the amount.</param>
    /// <exception cref="ArgumentNullException">Thrown if currency is null.</exception>
    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    /// <summary>
    /// Creates a Money instance from a decimal and string currency code.
    /// </summary>
    public static Money From(decimal amount, string currencyCode)
    {
        return new Money(amount, Currency.FromCode(currencyCode));
    }

    /// <summary>
    /// Represents a zero value for a specific currency.
    /// </summary>
    public static Money Zero(Currency currency) => new(0, currency);

    // Arithmetic Operators

    public static Money operator +(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money left, decimal multiplier)
    {
        return new Money(left.Amount * multiplier, left.Currency);
    }

    public static Money operator /(Money left, decimal divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Cannot divide money by zero.");
        }
        return new Money(left.Amount / divisor, left.Currency);
    }

    // Comparison Operators

    public static bool operator >(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return left.Amount >= right.Amount;
    }

    public static bool operator <=(Money left, Money right)
    {
        CheckCurrencyMatch(left, right);
        return left.Amount <= right.Amount;
    }

    /// <summary>
    /// Ensures that two Money objects have the same currency before operation.
    /// </summary>
    private static void CheckCurrencyMatch(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException($"Currency mismatch: Cannot perform operation on {left.Currency} and {right.Currency}.");
        }
    }

    public override string ToString() => $"{Amount:N2} {Currency}";
}