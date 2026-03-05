using System;
using System.Net.Http;
using EnterpriseMediator.Core.SharedKernel.Configuration;
using Polly;
using Polly.Extensions.Http;

namespace EnterpriseMediator.Core.SharedKernel.Extensions
{
    /// <summary>
    /// Factory for creating standard Polly resilience policies based on configuration.
    /// Used to configure HttpClient resilience across microservices.
    /// </summary>
    public static class PollyPolicyExtensions
    {
        /// <summary>
        /// Creates a Wait and Retry policy with exponential backoff.
        /// </summary>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(ResiliencyOptions options)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(
                    retryCount: options.RetryCount,
                    sleepDurationProvider: retryAttempt => 
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(new Random().Next(0, 100)),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        // In a real scenario, we might log this retry attempt via a injected logger if accessible,
                        // or rely on Polly's Context to pass logger.
                    });
        }

        /// <summary>
        /// Creates a Circuit Breaker policy.
        /// </summary>
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ResiliencyOptions options)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: options.ExceptionsAllowedBeforeBreaking,
                    durationOfBreak: TimeSpan.FromSeconds(options.DurationOfBreakSeconds)
                );
        }
    }
}