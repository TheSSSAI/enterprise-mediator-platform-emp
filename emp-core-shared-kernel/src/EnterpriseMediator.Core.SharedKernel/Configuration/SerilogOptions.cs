namespace EnterpriseMediator.Core.SharedKernel.Configuration;

/// <summary>
/// Configuration settings for structured logging via Serilog.
/// </summary>
public class SerilogOptions
{
    /// <summary>
    /// The minimum log level to record (e.g., Information, Warning, Error).
    /// </summary>
    public string MinimumLevel { get; set; } = "Information";

    /// <summary>
    /// Gets or sets a value indicating whether to enable console logging.
    /// </summary>
    public bool EnableConsoleLogging { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to write logs to a file.
    /// </summary>
    public bool EnableFileLogging { get; set; } = false;

    /// <summary>
    /// The path format for log files if file logging is enabled.
    /// </summary>
    public string LogFilePathFormat { get; set; } = "logs/log-.txt";

    /// <summary>
    /// The application name to enrich logs with.
    /// </summary>
    public string ApplicationName { get; set; } = "EnterpriseMediator";
}