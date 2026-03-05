using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.UserManagement.Domain.Aggregates.User;
using EnterpriseMediator.UserManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.UserManagement.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of the IUserRepository using Entity Framework Core.
    /// Manages persistence operations for the User aggregate root.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserDbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds a new user to the database context.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Users.AddAsync(user, cancellationToken);
                _logger.LogDebug("User {UserId} added to context state tracking.", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user {UserId} to context.", user.Id);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing user in the database context.
        /// </summary>
        /// <param name="user">The user entity to update.</param>
        public Task UpdateAsync(User user)
        {
            try
            {
                // In EF Core, if the entity is already tracked, Update isn't strictly necessary if properties were modified on the tracked instance.
                // However, calling Update ensures the entity state is set to Modified, which is useful for disconnected scenarios.
                _dbContext.Users.Update(user);
                _logger.LogDebug("User {UserId} marked for update in context.", user.Id);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId} in context.", user.Id);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// Includes related Roles to ensure RBAC capabilities are loaded.
        /// </summary>
        /// <param name="id">The GUID of the user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The User entity if found; otherwise, null.</returns>
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .Include(u => u.Roles) // Eager load roles for permission checks
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The User entity if found; otherwise, null.</returns>
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            // Emails are stored as Value Objects but mapped to a column. 
            // Assuming Value Conversion in UserConfiguration handles the comparison or we access the property.
            return await _dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
        }

        /// <summary>
        /// Checks if an email address is unique within the system.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the email is unique (does not exist); otherwise, false.</returns>
        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var exists = await _dbContext.Users
                .AnyAsync(u => u.Email.Value == email, cancellationToken);

            return !exists;
        }
    }
}