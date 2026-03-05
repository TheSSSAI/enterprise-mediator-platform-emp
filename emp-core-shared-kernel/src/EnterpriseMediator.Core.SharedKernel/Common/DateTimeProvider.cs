using System;
using EnterpriseMediator.Core.SharedKernel.Abstractions;

namespace EnterpriseMediator.Core.SharedKernel.Common
{
    /// <summary>
    /// Production-grade implementation of the DateTime provider to enable testability.
    /// Wraps the system static DateTime calls.
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTimeOffset OffsetNow => DateTimeOffset.Now;
        public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
    }
}