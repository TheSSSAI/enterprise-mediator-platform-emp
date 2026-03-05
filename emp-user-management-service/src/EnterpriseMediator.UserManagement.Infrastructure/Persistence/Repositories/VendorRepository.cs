using System;
using System.Threading;
using System.Threading.Tasks;
using EnterpriseMediator.UserManagement.Domain.Aggregates.Vendor;
using EnterpriseMediator.UserManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.UserManagement.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementation of the IVendorRepository using Entity Framework Core.
    /// Handles persistence for Vendor entities, including their skills and payment information.
    /// </summary>
    public class VendorRepository : IVendorRepository
    {
        private readonly UserDbContext _dbContext;
        private readonly ILogger<VendorRepository> _logger;

        public VendorRepository(UserDbContext dbContext, ILogger<VendorRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds a new vendor to the database context.
        /// </summary>
        /// <param name="vendor">The vendor entity to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task AddAsync(Vendor vendor, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Vendors.AddAsync(vendor, cancellationToken);
                _logger.LogDebug("Vendor {VendorId} added to context.", vendor.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add vendor {VendorId}.", vendor.Id);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a vendor by their unique identifier.
        /// Eager loads Skills and PaymentDetails to ensure the aggregate is fully consistent.
        /// </summary>
        /// <param name="id">The GUID of the vendor.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The Vendor entity if found; otherwise, null.</returns>
        public async Task<Vendor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // PaymentDetails is an Owned Entity, so it is loaded automatically by EF Core.
            // Skills is a collection, so we explicitly Include it.
            return await _dbContext.Vendors
                .Include(v => v.Skills)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing vendor in the database context.
        /// </summary>
        /// <param name="vendor">The vendor entity to update.</param>
        public Task UpdateAsync(Vendor vendor)
        {
            try
            {
                _dbContext.Vendors.Update(vendor);
                _logger.LogDebug("Vendor {VendorId} updated in context.", vendor.Id);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update vendor {VendorId}.", vendor.Id);
                throw;
            }
        }

        /// <summary>
        /// Checks if a vendor exists with the given ID.
        /// </summary>
        /// <param name="id">The vendor ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Vendors
                .AnyAsync(v => v.Id == id, cancellationToken);
        }
    }
}