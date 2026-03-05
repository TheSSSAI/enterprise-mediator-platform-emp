using MediatR;
using System;
using System.Collections.Generic;

namespace EnterpriseMediator.UserManagement.Application.Features.Vendors.Commands.CreateVendor;

/// <summary>
/// Command to create a new Vendor entity profile.
/// Vendors are created with a default status of "Pending Vetting".
/// </summary>
public sealed record CreateVendorCommand : IRequest<Guid>
{
    /// <summary>
    /// The legal company name of the vendor.
    /// </summary>
    public string CompanyName { get; init; } = string.Empty;

    /// <summary>
    /// The tax identification number (e.g., EIN, VAT).
    /// </summary>
    public string TaxId { get; init; } = string.Empty;

    /// <summary>
    /// The primary email address for the initial vendor contact.
    /// </summary>
    public string PrimaryContactEmail { get; init; } = string.Empty;

    /// <summary>
    /// The first name of the primary contact.
    /// </summary>
    public string PrimaryContactFirstName { get; init; } = string.Empty;

    /// <summary>
    /// The last name of the primary contact.
    /// </summary>
    public string PrimaryContactLastName { get; init; } = string.Empty;

    /// <summary>
    /// List of initial skill tags or expertise areas.
    /// </summary>
    public List<string> Skills { get; init; } = new();

    /// <summary>
    /// Street address line 1.
    /// </summary>
    public string AddressLine1 { get; init; } = string.Empty;

    /// <summary>
    /// Street address line 2 (optional).
    /// </summary>
    public string? AddressLine2 { get; init; }

    /// <summary>
    /// City name.
    /// </summary>
    public string City { get; init; } = string.Empty;

    /// <summary>
    /// State or province.
    /// </summary>
    public string State { get; init; } = string.Empty;

    /// <summary>
    /// Postal or ZIP code.
    /// </summary>
    public string PostalCode { get; init; } = string.Empty;

    /// <summary>
    /// Country code (ISO 2-letter).
    /// </summary>
    public string Country { get; init; } = string.Empty;
}