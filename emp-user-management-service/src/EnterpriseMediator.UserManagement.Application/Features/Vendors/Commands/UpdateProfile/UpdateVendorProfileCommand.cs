using MediatR;
using System;
using System.Collections.Generic;

namespace EnterpriseMediator.UserManagement.Application.Features.Vendors.Commands.UpdateProfile;

/// <summary>
/// Command to update an existing Vendor's profile information.
/// Includes support for updating address, skills, and sensitive payment details.
/// </summary>
public sealed record UpdateVendorProfileCommand : IRequest<bool>
{
    /// <summary>
    /// The unique identifier of the vendor to update.
    /// </summary>
    public Guid VendorId { get; init; }

    /// <summary>
    /// Updated company name (optional).
    /// </summary>
    public string? CompanyName { get; init; }

    /// <summary>
    /// Updated street address line 1 (optional).
    /// </summary>
    public string? AddressLine1 { get; init; }

    /// <summary>
    /// Updated street address line 2 (optional).
    /// </summary>
    public string? AddressLine2 { get; init; }

    /// <summary>
    /// Updated city (optional).
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// Updated state (optional).
    /// </summary>
    public string? State { get; init; }

    /// <summary>
    /// Updated postal code (optional).
    /// </summary>
    public string? PostalCode { get; init; }

    /// <summary>
    /// Updated country (optional).
    /// </summary>
    public string? Country { get; init; }

    /// <summary>
    /// Updated list of skills. If provided, replaces the existing list.
    /// </summary>
    public List<string>? Skills { get; init; }

    /// <summary>
    /// Payment provider name (e.g., "Wise", "Stripe") (optional).
    /// </summary>
    public string? PaymentProvider { get; init; }

    /// <summary>
    /// Payment account identifier (e.g., IBAN, Account Number) (optional).
    /// This field should be handled securely.
    /// </summary>
    public string? PaymentAccountNumber { get; init; }

    /// <summary>
    /// Bank routing or sort code (optional).
    /// </summary>
    public string? PaymentRoutingNumber { get; init; }
}