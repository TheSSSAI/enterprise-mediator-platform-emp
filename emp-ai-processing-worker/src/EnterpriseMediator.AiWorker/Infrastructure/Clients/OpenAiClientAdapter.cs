using Azure;
using Azure.AI.OpenAI;
using EnterpriseMediator.AiWorker.Application.Interfaces;
using EnterpriseMediator.AiWorker.Configuration;
using EnterpriseMediator.AiWorker.Features.SowProcessing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EnterpriseMediator.AiWorker.Infrastructure.Clients
{
    /// <summary>
    /// Implementation of the AI Extraction Service using Azure OpenAI SDK.
    /// Handles interaction with LLM for structured data extraction and embedding generation.
    /// </summary>
    public class OpenAiClientAdapter : IAiExtractionService
    {
        private readonly OpenAIClient _openAiClient;
        private readonly AiSettings _settings;
        private readonly ILogger<OpenAiClientAdapter> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public OpenAiClientAdapter(
            OpenAIClient openAiClient,
            IOptions<AiSettings> settings,
            ILogger<OpenAiClientAdapter> logger)
        {
            _openAiClient = openAiClient ?? throw new ArgumentNullException(nameof(openAiClient));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
        }

        /// <inheritdoc />
        public async Task<SowDataDto> ExtractStructuredDataAsync(string text, CancellationToken token)
        {
            try
            {
                _logger.LogInformation("Starting structured data extraction via OpenAI. Deployment: {Deployment}", _settings.DeploymentName);

                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new ArgumentException("Input text cannot be empty for extraction.", nameof(text));
                }

                // Define the system prompt to enforce JSON structure
                var chatMessages = new List<ChatRequestMessage>
                {
                    new ChatRequestSystemMessage(
                        "You are a specialized SOW analyzer. Extract the following fields from the Statement of Work text provided: " +
                        "'scope_summary' (string), 'required_skills' (array of strings), 'deliverables' (array of strings). " +
                        "Respond ONLY with a valid JSON object matching this schema."),
                    new ChatRequestUserMessage(text)
                };

                var options = new ChatCompletionsOptions(_settings.DeploymentName, chatMessages)
                {
                    Temperature = (float)0.2, // Low temperature for deterministic extraction
                    MaxTokens = 2000,
                    ResponseFormat = ChatCompletionsResponseFormat.JsonObject
                };

                Response<ChatCompletions> response = await _openAiClient.GetChatCompletionsAsync(options, token);
                var completion = response.Value.Choices[0].Message.Content;

                if (string.IsNullOrWhiteSpace(completion))
                {
                    _logger.LogError("OpenAI returned an empty completion content.");
                    throw new InvalidOperationException("Failed to extract data: Empty response from AI provider.");
                }

                _logger.LogDebug("Raw AI Response: {Response}", completion);

                var result = JsonSerializer.Deserialize<SowDataDto>(completion, _jsonOptions);

                if (result == null)
                {
                    throw new JsonException("Failed to deserialize AI response into SowDataDto.");
                }

                return result;
            }
            catch (RequestFailedException ex) when (ex.Status == 429)
            {
                _logger.LogWarning(ex, "OpenAI Rate Limit exceeded. Retry policy should handle this.");
                throw; // Rethrow for Polly
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during AI Data Extraction.");
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<float[]> GenerateEmbeddingsAsync(string text, CancellationToken token)
        {
            try
            {
                _logger.LogInformation("Generating vector embeddings via OpenAI.");

                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new ArgumentException("Input text cannot be empty for embedding generation.", nameof(text));
                }

                var options = new EmbeddingsOptions(_settings.EmbeddingDeploymentName, new List<string> { text });
                
                Response<Embeddings> response = await _openAiClient.GetEmbeddingsAsync(options, token);
                var embeddingItem = response.Value.Data.FirstOrDefault();

                if (embeddingItem == null)
                {
                    throw new InvalidOperationException("OpenAI returned no embedding data.");
                }

                return embeddingItem.Embedding.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Vector Embedding generation.");
                throw;
            }
        }
    }
}