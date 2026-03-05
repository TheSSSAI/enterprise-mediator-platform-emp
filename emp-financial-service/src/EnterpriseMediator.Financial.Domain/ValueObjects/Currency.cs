namespace EnterpriseMediator.Financial.Domain.ValueObjects;

/// <summary>
/// Value Object representing a currency.
/// Ensures that currency codes are valid 3-letter ISO 4217 codes.
/// Immutable and comparable.
/// </summary>
public record Currency
{
    public string Code { get; }

    /// <summary>
    /// Private constructor to enforce validation via factory method.
    /// </summary>
    private Currency(string code)
    {
        Code = code;
    }

    /// <summary>
    /// Creates a new Currency instance after validating the code format.
    /// </summary>
    /// <param name="code">The 3-letter ISO 4217 currency code (e.g., USD, EUR).</param>
    /// <returns>A valid Currency object.</returns>
    /// <exception cref="ArgumentException">Thrown when the code is null, empty, or not 3 characters.</exception>
    public static Currency FromCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Currency code cannot be empty.", nameof(code));
        }

        var normalizedCode = code.Trim().ToUpperInvariant();

        if (normalizedCode.Length != 3)
        {
            throw new ArgumentException($"Currency code must be exactly 3 characters. Received: {code}", nameof(code));
        }

        return new Currency(normalizedCode);
    }

    // Common Currencies
    public static readonly Currency USD = new("USD");
    public static readonly Currency EUR = new("EUR");
    public static readonly Currency GBP = new("GBP");

    public override string ToString() => Code;

    public static implicit operator string(Currency currency) => currency.Code;
}