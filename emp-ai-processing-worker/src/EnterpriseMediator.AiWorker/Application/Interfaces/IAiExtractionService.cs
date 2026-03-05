using EnterpriseMediator.AiWorker.Features.SowProcessing;

namespace EnterpriseMediator.AiWorker.Application.Interfaces;

/// <summary>
/// Defines the contract for AI services responsible for extracting structured data 
/// and generating vector embeddings from unstructured text.
/// Implementations should encapsulate interactions with LLM providers (e.g., Azure OpenAI).
/// </summary>
public interface IAiExtractionService
{
    /// <summary>
    /// Extracts structured data (scope, skills, deliverables) from the provided sanitized text.
    /// </summary>
    /// <param name="text">The sanitized text content of the SOW document.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>A DTO containing the extracted structured data.</returns>
    /// <exception cref="ArgumentException">Thrown when input text is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the AI service fails or returns invalid JSON.</exception>
    Task<SowDataDto> ExtractStructuredDataAsync(string text, CancellationToken token = default);

    /// <summary>
    /// Generates a vector embedding for the provided text input.
    /// Used for semantic search capabilities.
    /// </summary>
    /// <param name="text">The text content to vectorize (typically skills or scope).</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>An array of floating-point numbers representing the vector embedding.</returns>
    /// <exception cref="ArgumentException">Thrown when input text is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when embedding generation fails.</exception>
    Task<float[]> GenerateEmbeddingsAsync(string text, CancellationToken token = default);
}