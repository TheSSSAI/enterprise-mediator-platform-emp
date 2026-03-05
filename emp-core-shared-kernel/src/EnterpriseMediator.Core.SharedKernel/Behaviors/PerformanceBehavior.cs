using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnterpriseMediator.Core.SharedKernel.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior specifically focused on detecting and logging performance degradation.
    /// Separated from general logging to allow specific filtering or alerting on performance issues.
    /// </summary>
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        private readonly Stopwatch _timer;

        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            // Threshold for performance warning (e.g., 500ms)
            // This could be made configurable via IOptions<SharedKernelOptions> if dependencies allow
            if (elapsedMilliseconds > 500) 
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogWarning("EnterpriseMediator Long Running Request: {Name} ({ElapsedMilliseconds}ms) {@Request}",
                    requestName, elapsedMilliseconds, request);
            }

            return response;
        }
    }
}