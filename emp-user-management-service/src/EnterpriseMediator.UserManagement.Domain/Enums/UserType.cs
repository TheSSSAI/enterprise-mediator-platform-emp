using System;

namespace EnterpriseMediator.UserManagement.Domain.Enums
{
    /// <summary>
    /// Represents the broad classification of a user within the Enterprise Mediator Platform.
    /// This discriminator determines the base level of access and the specific profile type associated with the user account.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Internal employees or system administrators with elevated privileges and access to internal management tools.
        /// </summary>
        Internal = 0,

        /// <summary>
        /// External users representing Client entities who initiate projects and pay invoices.
        /// </summary>
        Client = 1,

        /// <summary>
        /// External users representing Vendor entities who submit proposals and execute project work.
        /// </summary>
        Vendor = 2
    }
}