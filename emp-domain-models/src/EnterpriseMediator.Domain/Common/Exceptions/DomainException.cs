using System;

namespace EnterpriseMediator.Domain.Common.Exceptions
{
    /// <summary>
    /// Base exception for all domain layer exceptions.
    /// Used to differentiate domain logic errors from infrastructure or system errors.
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}