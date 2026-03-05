using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.UserManagement.Domain.Aggregates.Client;

namespace EnterpriseMediator.UserManagement.Domain.Interfaces
{
    /// <summary>
    /// Repository contract for Client aggregate persistence.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Retrieves a client by their unique identifier.
        /// </summary>
        Task<Client?> GetByIdAsync(Guid id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a client by their primary contact email.
        /// </summary>
        Task<Client?> GetByEmailAsync(string email, CancellationToken ct = default);

        /// <summary>
        /// Adds a new client to the repository.
        /// </summary>
        Task AddAsync(Client client, CancellationToken ct = default);

        /// <summary>
        /// Updates an existing client in the repository.
        /// </summary>
        void Update(Client client);

        /// <summary>
        /// Retrieves a paginated list of all clients.
        /// </summary>
        Task<IEnumerable<Client>> ListAsync(int page, int pageSize, CancellationToken ct = default);

        /// <summary>
        /// Persists changes to the data store.
        /// </summary>
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}