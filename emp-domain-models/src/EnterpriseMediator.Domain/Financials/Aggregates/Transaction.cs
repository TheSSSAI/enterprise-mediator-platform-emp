using System;
using EnterpriseMediator.Domain.ClientManagement.Aggregates;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Financials.Enums;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.Shared.ValueObjects;
using EnterpriseMediator.Domain.VendorManagement.Aggregates;

namespace EnterpriseMediator.Domain.Financials.Aggregates;

/// <summary>
/// Represents an immutable record of a financial event in the system.
/// Corresponds to US-066 (Transaction Ledger).
/// </summary>
public class Transaction : Entity<Guid>
{
    public TransactionType Type { get; private set; }
    public Money Amount { get; private set; }
    public Money PlatformFee { get; private set; }
    public Money NetAmount { get; private set; }
    
    public ProjectId ProjectId { get; private set; }
    public ClientId? ClientId { get; private set; }
    public VendorId? VendorId { get; private set; }
    public InvoiceId? InvoiceId { get; private set; }
    
    // External reference ID from payment gateway (e.g., Stripe Charge ID, Wise Transfer ID)
    public string ExternalReferenceId { get; private set; }
    
    public DateTimeOffset Timestamp { get; private set; }
    public string Description { get; private set; }

    // EF Core
    protected Transaction() { }

    private Transaction(
        TransactionType type,
        Money amount,
        Money platformFee,
        ProjectId projectId,
        string externalReferenceId,
        string description,
        ClientId? clientId = null,
        VendorId? vendorId = null,
        InvoiceId? invoiceId = null)
    {
        if (amount.Amount < 0)
            throw new BusinessRuleValidationException("Transaction amount cannot be negative.");
        
        if (platformFee.Amount < 0)
            throw new BusinessRuleValidationException("Platform fee cannot be negative.");

        if (string.IsNullOrWhiteSpace(externalReferenceId))
            throw new BusinessRuleValidationException("External reference ID is required for auditability.");

        Id = Guid.NewGuid();
        Type = type;
        Amount = amount;
        PlatformFee = platformFee;
        
        // Calculate Net. For Payouts, Net = Amount. For Incoming Payments, Net = Amount - Fee?
        // This logic depends on the perspective. Here we store the raw values and calculate Net simply.
        // Assuming Amount is Gross.
        NetAmount = new Money(amount.Amount - platformFee.Amount, amount.Currency);

        ProjectId = projectId;
        ClientId = clientId;
        VendorId = vendorId;
        InvoiceId = invoiceId;
        ExternalReferenceId = externalReferenceId;
        Timestamp = DateTimeOffset.UtcNow;
        Description = description;
    }

    /// <summary>
    /// Factory method to record an incoming payment from a client.
    /// </summary>
    public static Transaction RecordClientPayment(
        ProjectId projectId,
        ClientId clientId,
        InvoiceId invoiceId,
        Money grossAmount,
        Money fee,
        string stripeChargeId,
        string description = "Client Invoice Payment")
    {
        return new Transaction(
            TransactionType.Payment,
            grossAmount,
            fee,
            projectId,
            stripeChargeId,
            description,
            clientId: clientId,
            invoiceId: invoiceId
        );
    }

    /// <summary>
    /// Factory method to record an outgoing payout to a vendor.
    /// </summary>
    public static Transaction RecordVendorPayout(
        ProjectId projectId,
        VendorId vendorId,
        Money payoutAmount,
        string wiseTransferId,
        string description = "Vendor Payout")
    {
        // For payouts, the fee is usually 0 or internal cost, but the Amount passed is what the vendor receives.
        return new Transaction(
            TransactionType.Payout,
            payoutAmount,
            new Money(0, payoutAmount.Currency),
            projectId,
            wiseTransferId,
            description,
            vendorId: vendorId
        );
    }

    /// <summary>
    /// Factory method to record a refund to a client.
    /// </summary>
    public static Transaction RecordClientRefund(
        ProjectId projectId,
        ClientId clientId,
        Money refundAmount,
        string stripeRefundId,
        string description = "Client Refund")
    {
        return new Transaction(
            TransactionType.Refund,
            refundAmount,
            new Money(0, refundAmount.Currency), 
            projectId,
            stripeRefundId,
            description,
            clientId: clientId
        );
    }
}