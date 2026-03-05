namespace EnterpriseMediator.AiWorker.Application.Interfaces;

/// <summary>
/// Defines the contract for services responsible for detecting and redacting 
/// Personally Identifiable Information (PII) from text.
/// Implementations should utilize specialized NLP services (e.g., AWS Comprehend).
/// </summary>
public interface IPiiSanitizationService
{
    /// <summary>
    /// Scans the input text for sensitive PII entities and replaces them with generic placeholders.
    /// </summary>
    /// <param name="input">The raw text content to sanitize.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>The sanitized text safe for downstream processing.</returns>
    /// <exception cref="ArgumentException">Thrown when input is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the sanitization service is unavailable.</exception>
    Task<string> SanitizeTextAsync(string input, CancellationToken token = default);
}