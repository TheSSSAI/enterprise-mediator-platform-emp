using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.UserManagement.Domain.Aggregates.Vendor;

namespace EnterpriseMediator.UserManagement.Domain.Interfaces
{
    /// <summary>
    /// Repository contract for Vendor aggregate persistence.
    /// </summary>
    public interface IVendorRepository
    {
        /// <summary>
        /// Retrieves a vendor by their unique identifier including skills and payment details.
        /// </summary>
        Task<Vendor?> GetByIdAsync(Guid id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a vendor by their primary contact email.
        /// </summary>
        Task<Vendor?> GetByEmailAsync(string email, CancellationToken ct = default);

        /// <summary>
        /// Adds a new vendor to the repository.
        /// </summary>
        Task AddAsync(Vendor vendor, CancellationToken ct = default);

        /// <summary>
        /// Updates an existing vendor in the repository.
        /// </summary>
        void Update(Vendor vendor);

        /// <summary>
        /// Retrieves a list of vendors matching a specific vetting status.
        /// </summary>
        Task<IEnumerable<Vendor>> ListByStatusAsync(string status, int page, int pageSize, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of vendors that possess a specific skill tag.
        /// </summary>
        Task<IEnumerable<Vendor>> GetBySkillAsync(string skillTag, CancellationToken ct = default);
        
        /// <summary>
        /// Persists changes to the data store.
        /// </summary>
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}