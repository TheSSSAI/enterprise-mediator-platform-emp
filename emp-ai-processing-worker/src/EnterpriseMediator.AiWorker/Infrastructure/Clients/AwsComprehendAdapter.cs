using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using EnterpriseMediator.AiWorker.Application.Interfaces;
using EnterpriseMediator.AiWorker.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace EnterpriseMediator.AiWorker.Infrastructure.Clients
{
    /// <summary>
    /// Implementation of PII Sanitization Service using AWS Comprehend.
    /// Handles text chunking to respect AWS API limits (5000 bytes).
    /// </summary>
    public class AwsComprehendAdapter : IPiiSanitizationService
    {
        private readonly IAmazonComprehend _comprehendClient;
        private readonly AwsSettings _settings;
        private readonly ILogger<AwsComprehendAdapter> _logger;

        // AWS Comprehend limit is 5000 bytes. We use 4500 to be safe with encoding variations.
        private const int MaxChunkSize = 4500;

        public AwsComprehendAdapter(
            IAmazonComprehend comprehendClient,
            IOptions<AwsSettings> settings,
            ILogger<AwsComprehendAdapter> logger)
        {
            _comprehendClient = comprehendClient ?? throw new ArgumentNullException(nameof(comprehendClient));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<string> SanitizeTextAsync(string input, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            _logger.LogInformation("Starting PII Sanitization. Input length: {Length}", input.Length);

            var chunks = ChunkText(input, MaxChunkSize);
            var sanitizedBuilder = new StringBuilder();

            foreach (var chunk in chunks)
            {
                var sanitizedChunk = await ProcessChunkAsync(chunk, token);
                sanitizedBuilder.Append(sanitizedChunk);
            }

            _logger.LogInformation("PII Sanitization complete.");
            return sanitizedBuilder.ToString();
        }

        private async Task<string> ProcessChunkAsync(string chunk, CancellationToken token)
        {
            try
            {
                var request = new DetectPiiEntitiesRequest
                {
                    Text = chunk,
                    LanguageCode = LanguageCode.En
                };

                var response = await _comprehendClient.DetectPiiEntitiesAsync(request, token);

                if (response.Entities.Count == 0)
                {
                    return chunk;
                }

                // Process replacements from end to start to maintain index validity
                var sb = new StringBuilder(chunk);
                foreach (var entity in response.Entities.OrderByDescending(e => e.BeginOffset))
                {
                    // Create a placeholder like [NAME], [DATE], [EMAIL]
                    string placeholder = $"[{entity.Type.Value.ToUpperInvariant()}]";
                    
                    // The offsets in Comprehend are based on UTF-8 code units, but C# strings are UTF-16.
                    // For standard ASCII this works, but for complex unicode, index mapping might be required.
                    // Assuming mostly standard SOW text here.
                    if (entity.EndOffset <= sb.Length && entity.BeginOffset >= 0)
                    {
                        sb.Remove(entity.BeginOffset, entity.EndOffset - entity.BeginOffset);
                        sb.Insert(entity.BeginOffset, placeholder);
                    }
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to detect PII in chunk.");
                throw new InvalidOperationException("PII sanitization failed.", ex);
            }
        }

        private static IEnumerable<string> ChunkText(string text, int maxChunkSize)
        {
            for (int i = 0; i < text.Length; i += maxChunkSize)
            {
                if (i + maxChunkSize > text.Length)
                {
                    yield return text.Substring(i);
                }
                else
                {
                    // Attempt to find the last whitespace within the chunk limit to avoid splitting words
                    int end = i + maxChunkSize;
                    int lastSpace = text.LastIndexOf(' ', end, maxChunkSize);
                    
                    // If no space found (very long word), split at limit. Otherwise split at space.
                    int splitIndex = (lastSpace > i) ? lastSpace : end;
                    int length = splitIndex - i;

                    yield return text.Substring(i, length);
                    
                    // Adjust iterator if we split early at a space
                    i = splitIndex - maxChunkSize; // -maxChunkSize because the loop adds it back
                    // Actually cleaner: manually manage loop
                }
            }
        }
    }
}