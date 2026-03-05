using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.Core.SharedKernel.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior that logs the execution of every request.
    /// Tracks execution time and logs request details for observability.
    /// </summary>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var uniqueId = System.Guid.NewGuid().ToString();

            // Log Request Start
            _logger.LogInformation("Begin Request Id:{UniqueId} Name:{RequestName} {@Request}", 
                uniqueId, requestName, request);

            var timer = new Stopwatch();
            timer.Start();

            try
            {
                var response = await next();

                timer.Stop();

                // Log Request Completion
                if (timer.ElapsedMilliseconds > 500)
                {
                    _logger.LogWarning("Long Running Request Id:{UniqueId} Name:{RequestName} ({ElapsedMilliseconds}ms) {@Request}", 
                        uniqueId, requestName, timer.ElapsedMilliseconds, request);
                }
                else
                {
                    _logger.LogInformation("End Request Id:{UniqueId} Name:{RequestName} ({ElapsedMilliseconds}ms)", 
                        uniqueId, requestName, timer.ElapsedMilliseconds);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                timer.Stop();
                _logger.LogError(ex, "Request Failure Id:{UniqueId} Name:{RequestName} ({ElapsedMilliseconds}ms) {@Request}", 
                    uniqueId, requestName, timer.ElapsedMilliseconds, request);
                throw;
            }
        }
    }
}