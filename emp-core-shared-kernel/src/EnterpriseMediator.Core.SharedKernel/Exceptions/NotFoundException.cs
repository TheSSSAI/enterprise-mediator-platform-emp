using System;

namespace EnterpriseMediator.Core.SharedKernel.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource or entity cannot be found.
    /// Maps typically to a 404 Not Found HTTP status code in the API layer.
    /// </summary>
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(string name, object key) 
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}