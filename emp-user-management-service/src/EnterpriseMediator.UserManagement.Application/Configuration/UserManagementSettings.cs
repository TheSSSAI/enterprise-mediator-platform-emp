using System.ComponentModel.DataAnnotations;

namespace EnterpriseMediator.UserManagement.Application.Configuration;

/// <summary>
/// Configuration settings for the User Management module.
/// Mapped from "UserManagement" section in appsettings.json.
/// </summary>
public class UserManagementSettings
{
    public const string SectionName = "UserManagement";

    /// <summary>
    /// The default role assigned to new client users upon registration.
    /// </summary>
    [Required]
    public string DefaultClientRole { get; set; } = "ClientAdmin";

    /// <summary>
    /// The default role assigned to new vendor users upon registration.
    /// </summary>
    [Required]
    public string DefaultVendorRole { get; set; } = "VendorContact";

    /// <summary>
    /// The default role assigned to new internal users.
    /// </summary>
    [Required]
    public string DefaultInternalRole { get; set; } = "Employee";

    /// <summary>
    /// Default vetting status for new vendors.
    /// </summary>
    public string DefaultVendorStatus { get; set; } = "PendingVetting";

    /// <summary>
    /// Enable or disable automatic welcome emails.
    /// </summary>
    public bool SendWelcomeEmail { get; set; } = true;
}