using System.ComponentModel.DataAnnotations;

namespace EnterpriseMediator.AiWorker.Configuration;

/// <summary>
/// Configuration settings for AI services integration (specifically Azure OpenAI).
/// Validated at startup to ensure all required connection details are present.
/// </summary>
public class AiSettings
{
    public const string SectionName = "Ai";

    /// <summary>
    /// The endpoint URL for the Azure OpenAI resource.
    /// </summary>
    [Required(ErrorMessage = "OpenAiEndpoint is required.")]
    [Url(ErrorMessage = "OpenAiEndpoint must be a valid URL.")]
    public string OpenAiEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// The API key for authenticating with the Azure OpenAI resource.
    /// </summary>
    [Required(ErrorMessage = "OpenAiKey is required.")]
    public string OpenAiKey { get; set; } = string.Empty;

    /// <summary>
    /// The name of the specific model deployment to use (e.g., "gpt-4", "text-embedding-ada-002").
    /// </summary>
    [Required(ErrorMessage = "DeploymentName is required.")]
    public string DeploymentName { get; set; } = "gpt-4";

    /// <summary>
    /// The name of the specific embedding model deployment (e.g., "text-embedding-ada-002").
    /// </summary>
    [Required(ErrorMessage = "EmbeddingDeploymentName is required.")]
    public string EmbeddingDeploymentName { get; set; } = "text-embedding-ada-002";
}