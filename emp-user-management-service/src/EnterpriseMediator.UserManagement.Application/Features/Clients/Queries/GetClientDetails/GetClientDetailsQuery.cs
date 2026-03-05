using MediatR;
using System;
using System.Collections.Generic;

namespace EnterpriseMediator.UserManagement.Application.Features.Clients.Queries.GetClientDetails;

/// <summary>
/// Query to retrieve detailed information about a specific client.
/// </summary>
/// <param name="ClientId">The unique identifier of the client.</param>
public sealed record GetClientDetailsQuery(Guid ClientId) : IRequest<ClientDetailsDto>;

/// <summary>
/// Data Transfer Object representing detailed client information.
/// </summary>
public record ClientDetailsDto
{
    public Guid Id { get; init; }
    public string CompanyName { get; init; } = string.Empty;
    public string Industry { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    
    public ClientAddressDto Address { get; init; } = new();
    public List<ClientContactDto> Contacts { get; init; } = new();
}

/// <summary>
/// DTO for client address information.
/// </summary>
public record ClientAddressDto
{
    public string AddressLine1 { get; init; } = string.Empty;
    public string? AddressLine2 { get; init; }
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
}

/// <summary>
/// DTO for client contact information.
/// </summary>
public record ClientContactDto
{
    public Guid UserId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}