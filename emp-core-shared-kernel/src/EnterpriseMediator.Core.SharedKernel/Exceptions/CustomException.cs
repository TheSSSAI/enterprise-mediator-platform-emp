using System;
using System.Collections.Generic;
using System.Net;

namespace EnterpriseMediator.Core.SharedKernel.Exceptions;

/// <summary>
/// Base exception type for all domain-specific and application-specific exceptions.
/// Allows mapping to HTTP status codes and providing detailed error information.
/// </summary>
public abstract class CustomException : Exception
{
    protected CustomException(string message, Exception? innerException = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorDetails = new List<string>();
    }

    protected CustomException(string message, IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorDetails = errors ?? new List<string>();
    }

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the collection of detailed error messages.
    /// </summary>
    public IEnumerable<string> ErrorDetails { get; }
}