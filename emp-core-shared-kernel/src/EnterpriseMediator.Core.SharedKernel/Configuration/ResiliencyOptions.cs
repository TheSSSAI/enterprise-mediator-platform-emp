namespace EnterpriseMediator.Core.SharedKernel.Configuration;

/// <summary>
/// Configuration settings for system resiliency patterns (Retries, Circuit Breakers).
/// </summary>
public class ResiliencyOptions
{
    /// <summary>
    /// The number of times to retry a failed HTTP request.
    /// </summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// The number of exceptions allowed before breaking the circuit.
    /// </summary>
    public int CircuitBreakerExceptionsAllowedBeforeBreaking { get; set; } = 5;

    /// <summary>
    /// The duration in seconds the circuit remains open before allowing test requests.
    /// </summary>
    public int CircuitBreakerDurationOfBreakInSeconds { get; set; } = 30;

    /// <summary>
    /// The timeout duration for HTTP requests in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 10;
}