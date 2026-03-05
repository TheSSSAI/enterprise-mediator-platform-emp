using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMediator.ProjectManagement.WebAPI.Middleware;

/// <summary>
/// Global exception handler middleware that intercepts unhandled exceptions 
/// and converts them into standardized ProblemDetails responses (RFC 7807).
/// Ensures sensitive stack traces are not exposed in production.
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Attempts to handle the exception and write a standardized JSON response.
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        var (statusCode, title, detail) = MapExceptionToResponse(exception);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = GetTypeUrl(statusCode),
            Instance = httpContext.Request.Path
        };

        // Add extensions for validation errors if applicable
        if (exception is FluentValidation.ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static (int StatusCode, string Title, string Detail) MapExceptionToResponse(Exception exception)
    {
        return exception switch
        {
            // Handle Validation Exceptions (FluentValidation)
            FluentValidation.ValidationException ve => 
                (StatusCodes.Status400BadRequest, "Validation Failure", "One or more validation errors occurred."),

            // Handle Domain/Business Rule Violations (Assuming a base DomainException type exists, or generic ArgumentException)
            ArgumentException ae => 
                (StatusCodes.Status400BadRequest, "Invalid Argument", ae.Message),
            
            InvalidOperationException ioe => 
                (StatusCodes.Status409Conflict, "Invalid Operation", ioe.Message),

            // Handle Not Found Resources
            KeyNotFoundException knf => 
                (StatusCodes.Status404NotFound, "Resource Not Found", knf.Message),
            
            // Handle Data Annotations (if used alongside FluentValidation)
            ValidationException ve => 
                (StatusCodes.Status400BadRequest, "Validation Error", ve.Message),

            // Handle Authorization Errors (if bubbled up)
            UnauthorizedAccessException => 
                (StatusCodes.Status401Unauthorized, "Unauthorized", "You are not authorized to access this resource."),

            // Default / Unhandled
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred. Please contact support.")
        };
    }

    private static string GetTypeUrl(int statusCode)
    {
        return statusCode switch
        {
            400 => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            401 => "https://tools.ietf.org/html/rfc7235#section-3.1",
            403 => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            404 => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            409 => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            500 => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            _ => "https://tools.ietf.org/html/rfc7231"
        };
    }
}