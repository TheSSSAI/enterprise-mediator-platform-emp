using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EnterpriseMediator.AiWorker.Application.Interfaces;
using EnterpriseMediator.AiWorker.Features.SowProcessing;

namespace EnterpriseMediator.AiWorker.Features.SowProcessing
{
    /// <summary>
    /// Encapsulates the core business logic pipeline for SOW document processing.
    /// Orchestrates parsing, PII sanitization, AI data extraction, and vector embedding generation.
    /// </summary>
    public class SowProcessingOrchestrator
    {
        private readonly IPiiSanitizationService _piiSanitizationService;
        private readonly IAiExtractionService _aiExtractionService;
        private readonly IDocumentParser _documentParser;
        private readonly ILogger<SowProcessingOrchestrator> _logger;

        public SowProcessingOrchestrator(
            IPiiSanitizationService piiSanitizationService,
            IAiExtractionService aiExtractionService,
            IDocumentParser documentParser,
            ILogger<SowProcessingOrchestrator> logger)
        {
            _piiSanitizationService = piiSanitizationService ?? throw new ArgumentNullException(nameof(piiSanitizationService));
            _aiExtractionService = aiExtractionService ?? throw new ArgumentNullException(nameof(aiExtractionService));
            _documentParser = documentParser ?? throw new ArgumentNullException(nameof(documentParser));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Orchestrates the full AI processing pipeline for a given document stream.
        /// </summary>
        /// <param name="fileStream">The raw file stream of the SOW document.</param>
        /// <param name="fileExtension">The file extension (e.g., .docx, .pdf) to determine parsing strategy.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result object containing the sanitized text, extracted structured data, and vector embeddings.</returns>
        public async Task<SowProcessingResult> ProcessAsync(Stream fileStream, string fileExtension, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting SOW processing pipeline. FileType: {FileType}", fileExtension);

            try
            {
                // Step 1: Document Parsing (Stream -> Text)
                // We reset stream position to ensure we read from the start
                if (fileStream.CanSeek)
                {
                    fileStream.Position = 0;
                }

                _logger.LogDebug("Parsing document text...");
                string rawText = await _documentParser.ParseTextAsync(fileStream, fileExtension, cancellationToken);

                if (string.IsNullOrWhiteSpace(rawText))
                {
                    _logger.LogWarning("Document parsing resulted in empty text.");
                    throw new InvalidOperationException("The uploaded SOW document contains no extractable text.");
                }

                // Step 2: PII Sanitization
                _logger.LogDebug("Sanitizing text for PII...");
                string sanitizedText = await _piiSanitizationService.SanitizeTextAsync(rawText, cancellationToken);

                // Step 3: Structured Data Extraction (using Sanitized Text)
                _logger.LogDebug("Extracting structured data via AI...");
                SowDataDto extractedData = await _aiExtractionService.ExtractStructuredDataAsync(sanitizedText, cancellationToken);

                if (extractedData == null)
                {
                    throw new InvalidOperationException("AI service returned null data for structure extraction.");
                }

                // Step 4: Vector Embedding Generation (using Sanitized Text context)
                // We construct a representative string for embedding, typically combining key fields if not using the full text
                // However, for semantic search on the SOW, embedding the sanitized text or a summary is common. 
                // Here we embed the full sanitized text for maximum context match.
                _logger.LogDebug("Generating vector embeddings...");
                float[] embeddings = await _aiExtractionService.GenerateEmbeddingsAsync(sanitizedText, cancellationToken);

                if (embeddings == null || embeddings.Length == 0)
                {
                    throw new InvalidOperationException("AI service failed to generate vector embeddings.");
                }

                _logger.LogInformation("SOW processing pipeline completed successfully.");

                return new SowProcessingResult
                {
                    SanitizedText = sanitizedText,
                    ExtractedData = extractedData,
                    VectorEmbeddings = embeddings
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during SOW processing pipeline.");
                throw; // Rethrow to be handled by the Command Handler which manages state
            }
        }
    }

    /// <summary>
    /// Represents the aggregated result of the SOW processing pipeline.
    /// </summary>
    public class SowProcessingResult
    {
        public string SanitizedText { get; set; }
        public SowDataDto ExtractedData { get; set; }
        public float[] VectorEmbeddings { get; set; }
    }
}