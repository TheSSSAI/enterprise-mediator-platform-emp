using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.ClientManagement.Aggregates;

/// <summary>
/// Represents a point of contact within a Client organization.
/// </summary>
public class ClientContact : Entity<Guid>
{
    public ClientId ClientId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public string? JobTitle { get; private set; }
    public bool IsPrimary { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    // EF Core constructor
    protected ClientContact() { }

    private ClientContact(
        Guid id,
        ClientId clientId,
        string firstName,
        string lastName,
        Email email,
        PhoneNumber? phoneNumber,
        string? jobTitle,
        bool isPrimary)
    {
        Id = id;
        ClientId = clientId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        JobTitle = jobTitle;
        IsPrimary = isPrimary;
        CreatedAt = DateTime.UtcNow;
    }

    public static ClientContact Create(
        ClientId clientId,
        string firstName,
        string lastName,
        Email email,
        PhoneNumber? phoneNumber = null,
        string? jobTitle = null,
        bool isPrimary = false)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new BusinessRuleValidationException("First name is required.");
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new BusinessRuleValidationException("Last name is required.");

        if (email is null)
            throw new ArgumentNullException(nameof(email));

        return new ClientContact(
            Guid.NewGuid(),
            clientId,
            firstName.Trim(),
            lastName.Trim(),
            email,
            phoneNumber,
            jobTitle?.Trim(),
            isPrimary);
    }

    public void UpdateDetails(
        string firstName,
        string lastName,
        PhoneNumber? phoneNumber,
        string? jobTitle)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new BusinessRuleValidationException("First name is required.");
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new BusinessRuleValidationException("Last name is required.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = phoneNumber;
        JobTitle = jobTitle?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPrimaryStatus(bool isPrimary)
    {
        IsPrimary = isPrimary;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(Email newEmail)
    {
        Email = newEmail ?? throw new ArgumentNullException(nameof(newEmail));
        UpdatedAt = DateTime.UtcNow;
    }
}