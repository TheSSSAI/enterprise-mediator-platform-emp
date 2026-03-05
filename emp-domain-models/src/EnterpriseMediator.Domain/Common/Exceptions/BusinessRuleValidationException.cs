using System;

namespace EnterpriseMediator.Domain.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when a business rule or invariant is violated.
    /// </summary>
    public class BusinessRuleValidationException : DomainException
    {
        public string Details { get; }

        public BusinessRuleValidationException(string message) 
            : base(message)
        {
            Details = message;
        }

        public BusinessRuleValidationException(string message, string details) 
            : base(message)
        {
            Details = details;
        }

        public BusinessRuleValidationException(string message, Exception innerException) 
            : base(message, innerException)
        {
            Details = message;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Details: {Details}";
        }
    }
}