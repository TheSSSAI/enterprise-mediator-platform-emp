using MediatR;
using System;

namespace EnterpriseMediator.UserManagement.Application.Features.Clients.Commands.CreateClient;

/// <summary>
/// Command to create a new Client entity profile.
/// This establishes the company record and optionally creates the primary contact user.
/// </summary>
public sealed record CreateClientCommand : IRequest<Guid>
{
    /// <summary>
    /// The legal company name of the client.
    /// </summary>
    public string CompanyName { get; init; } = string.Empty;

    /// <summary>
    /// The industry sector of the client.
    /// </summary>
    public string Industry { get; init; } = string.Empty;

    /// <summary>
    /// The primary email address for the initial admin user of this client.
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