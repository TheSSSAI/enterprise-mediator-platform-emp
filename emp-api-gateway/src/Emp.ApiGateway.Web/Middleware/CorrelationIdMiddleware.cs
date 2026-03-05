using Microsoft.Extensions.Primitives;

namespace Emp.ApiGateway.Web.Middleware
{
    /// <summary>
    /// Middleware that ensures every request has a Correlation ID.
    /// Reads 'X-Correlation-ID' from incoming headers or generates a new one.
    /// Adds the ID to the Response headers and the HttpContext items for downstream propagation.
    /// </summary>
    public class CorrelationIdMiddleware : IMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(ILogger<CorrelationIdMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var correlationId = GetCorrelationId(context);

            // Add correlation ID to items for retrieval during request scope
            context.Items[CorrelationIdHeaderName] = correlationId;

            // Add correlation ID to response headers
            context.Response.OnStarting(() =>
            {
                context.Response.Headers[CorrelationIdHeaderName] = new StringValues(correlationId);
                return Task.CompletedTask;
            });

            // Add correlation ID to logging scope
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
            {
                await next(context);
            }
        }

        private string GetCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationId))
            {
                return correlationId.ToString();
            }

            return Guid.NewGuid().ToString();
        }
    }
}