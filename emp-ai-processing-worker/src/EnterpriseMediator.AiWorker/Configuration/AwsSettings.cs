using System.ComponentModel.DataAnnotations;

namespace EnterpriseMediator.AiWorker.Configuration;

/// <summary>
/// Configuration settings for AWS infrastructure services (S3, Comprehend).
/// Validated at startup to ensure connectivity parameters are available.
/// </summary>
public class AwsSettings
{
    public const string SectionName = "Aws";

    /// <summary>
    /// The AWS Region system identifier (e.g., "us-east-1").
    /// </summary>
    [Required(ErrorMessage = "AWS Region is required.")]
    public string Region { get; set; } = "us-east-1";

    /// <summary>
    /// The name of the S3 bucket where SOW documents are stored.
    /// </summary>
    [Required(ErrorMessage = "S3BucketName is required.")]
    public string S3BucketName { get; set; } = string.Empty;

    /// <summary>
    /// Optional access key ID. If not provided, the SDK will fallback to the default credential chain (e.g., Instance Roles).
    /// </summary>
    public string? AccessKey { get; set; }

    /// <summary>
    /// Optional secret access key. If not provided, the SDK will fallback to the default credential chain.
    /// </summary>
    public string? SecretKey { get; set; }
}