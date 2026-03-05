using System;
using EnterpriseMediator.Financial.Domain.Interfaces;

namespace EnterpriseMediator.Financial.Infrastructure.Services
{
    /// <summary>
    /// Infrastructure implementation of the domain date/time abstraction.
    /// Enables testability by decoupling the system from the system clock.
    /// </summary>
    public class DateTimeProvider : IDateTime
    {
        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Gets the current date and time in the system's local time zone.
        /// Usage of this property is discouraged in favor of UtcNow for consistency.
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Gets the current date (time component set to midnight) in UTC.
        /// </summary>
        public DateTime Today => DateTime.UtcNow.Date;
    }
}