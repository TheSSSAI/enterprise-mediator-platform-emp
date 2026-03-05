namespace EnterpriseMediator.Core.SharedKernel.Configuration;

/// <summary>
/// Root configuration object for the Shared Kernel library settings.
/// </summary>
public class SharedKernelOptions
{
    public const string SectionName = "SharedKernel";

    /// <summary>
    /// Gets or sets the resiliency options (Polly).
    /// </summary>
    public ResiliencyOptions Resiliency { get; set; } = new();

    /// <summary>
    /// Gets or sets the Serilog logging options.
    /// </summary>
    public SerilogOptions Serilog { get; set; } = new();
}