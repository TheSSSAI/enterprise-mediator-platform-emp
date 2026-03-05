using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.UserManagement.Domain.Aggregates.User;

namespace EnterpriseMediator.UserManagement.Domain.Interfaces
{
    /// <summary>
    /// Repository contract for User aggregate persistence.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);

        /// <summary>
        /// Checks if a user exists with the given email.
        /// </summary>
        Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        Task AddAsync(User user, CancellationToken ct = default);

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        void Update(User user);

        /// <summary>
        /// Persists changes to the data store.
        /// Note: Often handled by a UnitOfWork, but included here for completeness of the repository contract scope.
        /// </summary>
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}