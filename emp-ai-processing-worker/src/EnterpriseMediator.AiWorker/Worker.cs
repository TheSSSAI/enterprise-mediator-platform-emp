using System.Diagnostics;

namespace EnterpriseMediator.AiWorker;

/// <summary>
/// A background worker service that monitors the health and lifecycle of the AI Processing application.
/// While MassTransit handles the actual message consumption loop, this worker provides 
/// operational visibility, heartbeat logging, and graceful shutdown handling.
/// </summary>
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    
    // Configurable heartbeat interval
    private static readonly TimeSpan HeartbeatInterval = TimeSpan.FromMinutes(1);

    public Worker(
        ILogger<Worker> logger,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _hostApplicationLifetime = hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));
    }

    /// <summary>
    /// Executes the long-running worker logic.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("AI Processing Worker Service is starting at: {time}", DateTimeOffset.Now);

        // Register lifecycle callbacks for better observability
        _hostApplicationLifetime.ApplicationStarted.Register(() => 
            _logger.LogInformation("AI Processing Worker: Application started and ready to process messages."));
        
        _hostApplicationLifetime.ApplicationStopping.Register(() => 
            _logger.LogInformation("AI Processing Worker: Application is stopping. Finishing current operations..."));

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Log operational heartbeat with memory usage metrics
                LogHeartbeat();

                // Wait for the next heartbeat interval
                // We use Task.Delay with the cancellation token to ensure we wake up immediately on shutdown
                await Task.Delay(HeartbeatInterval, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Normal shutdown behavior, ignore
            _logger.LogInformation("AI Processing Worker received cancellation signal.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AI Processing Worker encountered a critical error in the monitoring loop.");
            
            // In a critical failure of the monitoring loop, we might want to stop the application
            // depending on the severity. Here we log and exit the loop, but the host stays up
            // if MassTransit is still healthy.
        }
        finally
        {
            _logger.LogInformation("AI Processing Worker Service is stopped at: {time}", DateTimeOffset.Now);
        }
    }

    /// <summary>
    /// Logs system health metrics to structured logs.
    /// </summary>
    private void LogHeartbeat()
    {
        var process = Process.GetCurrentProcess();
        var memoryUsageMb = process.WorkingSet64 / 1024 / 1024;
        
        _logger.LogInformation(
            "Worker Heartbeat: {time} | Status: Healthy | Memory Usage: {memory} MB | Threads: {threads}", 
            DateTimeOffset.Now, 
            memoryUsageMb,
            process.Threads.Count);
    }
}