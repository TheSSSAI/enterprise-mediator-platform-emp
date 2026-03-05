using System;
using EnterpriseMediator.Financial.Domain.ValueObjects;

namespace EnterpriseMediator.Financial.Domain.Services
{
    /// <summary>
    /// Represents the breakdown of an invoice calculation.
    /// </summary>
    public record InvoiceCalculationResult
    {
        /// <summary>
        /// The base amount for the project/services.
        /// </summary>
        public Money BaseAmount { get; init; }

        /// <summary>
        /// The calculated platform fee/margin.
        /// </summary>
        public Money PlatformFee { get; init; }

        /// <summary>
        /// The calculated tax amount.
        /// </summary>
        public Money TaxAmount { get; init; }

        /// <summary>
        /// The total amount to be billed to the client (Base + Fee + Tax).
        /// </summary>
        public Money TotalClientAmount { get; init; }

        /// <summary>
        /// The effective percentage used for the margin calculation.
        /// </summary>
        public decimal MarginPercentageApplied { get; init; }

        /// <summary>
        /// The effective percentage used for the tax calculation.
        /// </summary>
        public decimal TaxPercentageApplied { get; init; }
    }

    /// <summary>
    /// Domain Service responsible for encapsulating the complex business logic of 
    /// calculating invoice totals, margins, and taxes. This ensures that financial 
    /// calculation rules are centralized and consistent.
    /// </summary>
    public class InvoiceCalculationService
    {
        /// <summary>
        /// Calculates the detailed breakdown of an invoice based on project cost and configuration.
        /// </summary>
        /// <param name="projectAmount">The base cost of the project or milestone.</param>
        /// <param name="marginPercentage">The platform margin percentage (e.g., 10 for 10%).</param>
        /// <param name="taxPercentage">The applicable tax percentage (e.g., 20 for 20% VAT).</param>
        /// <returns>A detailed breakdown of the financial components.</returns>
        /// <exception cref="ArgumentNullException">Thrown if projectAmount is null.</exception>
        /// <exception cref="ArgumentException">Thrown if percentages are negative.</exception>
        public InvoiceCalculationResult CalculateInvoiceBreakdown(
            Money projectAmount, 
            decimal marginPercentage, 
            decimal taxPercentage)
        {
            if (projectAmount == null)
            {
                throw new ArgumentNullException(nameof(projectAmount), "Project amount cannot be null.");
            }

            if (marginPercentage < 0)
            {
                throw new ArgumentException("Margin percentage cannot be negative.", nameof(marginPercentage));
            }

            if (taxPercentage < 0)
            {
                throw new ArgumentException("Tax percentage cannot be negative.", nameof(taxPercentage));
            }

            var currency = projectAmount.Currency;

            // 1. Calculate Platform Fee (Margin)
            // Logic: Margin is applied on top of the base project amount.
            // Formula: Fee = Base * (Margin / 100)
            decimal rawFee = projectAmount.Amount * (marginPercentage / 100m);
            // Round to 2 decimal places using MidpointRounding.AwayFromZero for financial precision standards
            decimal roundedFee = Math.Round(rawFee, 2, MidpointRounding.AwayFromZero);
            var platformFee = Money.From(roundedFee, currency);

            // 2. Calculate Taxable Subtotal
            // Logic: Typically taxes are applied to the Sum of (Base + Fee)
            decimal taxableAmount = projectAmount.Amount + platformFee.Amount;

            // 3. Calculate Tax
            // Formula: Tax = (Base + Fee) * (Tax / 100)
            decimal rawTax = taxableAmount * (taxPercentage / 100m);
            decimal roundedTax = Math.Round(rawTax, 2, MidpointRounding.AwayFromZero);
            var taxAmount = Money.From(roundedTax, currency);

            // 4. Calculate Total
            // Formula: Total = Base + Fee + Tax
            var totalAmount = Money.From(taxableAmount + taxAmount.Amount, currency);

            return new InvoiceCalculationResult
            {
                BaseAmount = projectAmount,
                PlatformFee = platformFee,
                TaxAmount = taxAmount,
                TotalClientAmount = totalAmount,
                MarginPercentageApplied = marginPercentage,
                TaxPercentageApplied = taxPercentage
            };
        }

        /// <summary>
        /// Validates if a calculated invoice result is financially sound (e.g., non-negative totals).
        /// </summary>
        /// <param name="result">The calculation result to validate.</param>
        /// <returns>True if valid, otherwise false.</returns>
        public bool ValidateCalculation(InvoiceCalculationResult result)
        {
            if (result == null) return false;

            // Basic sanity checks
            bool isCurrencyConsistent = 
                result.BaseAmount.Currency == result.PlatformFee.Currency &&
                result.BaseAmount.Currency == result.TaxAmount.Currency &&
                result.BaseAmount.Currency == result.TotalClientAmount.Currency;

            if (!isCurrencyConsistent) return false;

            bool isTotalCorrect = 
                result.TotalClientAmount.Amount == 
                (result.BaseAmount.Amount + result.PlatformFee.Amount + result.TaxAmount.Amount);

            bool areAmountsPositive = 
                result.BaseAmount.Amount >= 0 &&
                result.PlatformFee.Amount >= 0 &&
                result.TaxAmount.Amount >= 0 &&
                result.TotalClientAmount.Amount >= 0;

            return isTotalCorrect && areAmountsPositive;
        }
    }
}