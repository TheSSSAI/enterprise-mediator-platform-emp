using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using EnterpriseMediator.AiWorker.Application.Interfaces;
using EnterpriseMediator.AiWorker.Infrastructure.Messaging;

namespace EnterpriseMediator.AiWorker.Features.SowProcessing
{
    /// <summary>
    /// Handles the ProcessSowCommand triggered by the message bus.
    /// Orchestrates the retrieval of the file, invocation of the processing pipeline, 
    /// and persistence of the results using the Repository pattern.
    /// </summary>
    public class ProcessSowHandler : IRequestHandler<ProcessSowCommand, bool>
    {
        private readonly ISowRepository _sowRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly SowProcessingOrchestrator _orchestrator;
        private readonly SowProcessingValidator _validator;
        private readonly EventPublisher _eventPublisher;
        private readonly ILogger<ProcessSowHandler> _logger;

        public ProcessSowHandler(
            ISowRepository sowRepository,
            IFileStorageService fileStorageService,
            SowProcessingOrchestrator orchestrator,
            SowProcessingValidator validator,
            EventPublisher eventPublisher,
            ILogger<ProcessSowHandler> logger)
        {
            _sowRepository = sowRepository ?? throw new ArgumentNullException(nameof(sowRepository));
            _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
            _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ProcessSowCommand command, CancellationToken cancellationToken)
        {
            using var scope = _logger.BeginScope("Handling ProcessSowCommand for SowId: {SowId}", command.SowId);
            _logger.LogInformation("Starting SOW processing handler.");

            try
            {
                // 1. Validation
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    _logger.LogError("Invalid command received. Errors: {Errors}", string.Join(", ", validationResult.Errors));
                    return false; // Or throw ValidationException depending on strategy
                }

                // 2. Retrieve Entity & Idempotency Check
                var sowEntity = await _sowRepository.GetByIdAsync(command.SowId, cancellationToken);
                if (sowEntity == null)
                {
                    _logger.LogError("SOW entity not found in database.");
                    return false;
                }

                // Idempotency: If already processed or processing, we might want to skip or force retry depending on policy.
                // Assuming "Processing" might indicate a stuck job from a crash, but "Processed" is final.
                if (sowEntity.Status == "Processed")
                {
                    _logger.LogWarning("SOW is already marked as Processed. Skipping.");
                    return true;
                }

                // 3. Update Status to Processing
                sowEntity.Status = "Processing";
                sowEntity.UpdatedAt = DateTime.UtcNow;
                await _sowRepository.UpdateAsync(sowEntity, cancellationToken);
                
                // 4. Retrieve File Stream
                // Note: FileKey is expected to be the S3 object key
                _logger.LogDebug("Retrieving file stream from storage using Key: {FileKey}", command.FileKey);
                using var fileStream = await _fileStorageService.GetFileStreamAsync(command.FileKey, cancellationToken);
                
                if (fileStream == null || fileStream.Length == 0)
                {
                    throw new FileNotFoundException($"File stream is empty or null for key: {command.FileKey}");
                }

                // Determine extension for parser
                string extension = Path.GetExtension(command.FileKey).ToLowerInvariant();

                // 5. Execute Processing Pipeline
                var result = await _orchestrator.ProcessAsync(fileStream, extension, cancellationToken);

                // 6. Update Entity with Results
                sowEntity.SanitizedContent = result.SanitizedText;
                sowEntity.ExtractedDataJson = System.Text.Json.JsonSerializer.Serialize(result.ExtractedData);
                sowEntity.VectorEmbeddings = result.VectorEmbeddings;
                sowEntity.Status = "Processed";
                sowEntity.ProcessedAt = DateTime.UtcNow;

                // 7. Persist Changes
                await _sowRepository.UpdateAsync(sowEntity, cancellationToken);
                _logger.LogInformation("SOW data persisted successfully.");

                // 8. Publish Success Event
                await _eventPublisher.PublishSowProcessedAsync(new SowProcessedEvent 
                { 
                    SowId = command.SowId, 
                    ProjectId = sowEntity.ProjectId, 
                    Success = true 
                }, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical failure processing SOW.");

                // Failure Handling / Compensating Transaction
                try
                {
                    // Re-fetch to ensure we aren't working on stale tracked entity if context allows, 
                    // or just use existing reference if valid.
                    var failedEntity = await _sowRepository.GetByIdAsync(command.SowId, cancellationToken);
                    if (failedEntity != null)
                    {
                        failedEntity.Status = "Failed";
                        failedEntity.ErrorMessage = ex.Message;
                        failedEntity.UpdatedAt = DateTime.UtcNow;
                        await _sowRepository.UpdateAsync(failedEntity, cancellationToken);
                    }

                    await _eventPublisher.PublishSowFailedAsync(new SowFailedEvent
                    {
                        SowId = command.SowId,
                        ErrorCode = "PROCESSING_EXCEPTION",
                        ErrorMessage = ex.Message
                    }, cancellationToken);
                }
                catch (Exception persistenceEx)
                {
                    // If we can't even save the failure state, we log specifically for that.
                    _logger.LogCritical(persistenceEx, "Failed to persist 'Failed' state for SOW.");
                }

                // Depending on the messaging infrastructure (MassTransit), throwing here triggers the retry policy/DLQ.
                // We throw to ensure the message infrastructure knows this failed.
                throw;
            }
        }
    }
}