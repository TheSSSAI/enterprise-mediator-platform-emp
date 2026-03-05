using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using EnterpriseMediator.AiWorker.Features.SowProcessing;

// Assuming the existence of this shared contract based on Integration Specifications.
// If this were a monolithic generation, this record would be in a shared definitions file.
// For compilation in this specific context, if the reference is missing, this file assumes
// the contract library is referenced in the .csproj.
using EnterpriseMediator.Contracts; 

namespace EnterpriseMediator.AiWorker.Infrastructure.Messaging;

/// <summary>
/// Consumes SowUploadedEvent messages from the message bus and orchestrates 
/// the processing workflow via the application layer.
/// </summary>
public class SowUploadedConsumer : IConsumer<SowUploadedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<SowUploadedConsumer> _logger;

    public SowUploadedConsumer(
        IMediator mediator,
        ILogger<SowUploadedConsumer> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Handles the consumption of the SowUploadedEvent.
    /// Acts as an adapter between the Message Bus and the Application Logic (MediatR).
    /// </summary>
    /// <param name="context">The consumption context providing message data and cancellation tokens.</param>
    public async Task Consume(ConsumeContext<SowUploadedEvent> context)
    {
        var sowId = context.Message.SowId;
        var projectId = context.Message.ProjectId;
        var fileKey = context.Message.S3ObjectKey;
        var correlationId = context.CorrelationId ?? Guid.NewGuid();

        // Structured logging scope to attach correlation context to all logs within this scope
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["SowId"] = sowId,
            ["ProjectId"] = projectId,
            ["CorrelationId"] = correlationId,
            ["MessageId"] = context.MessageId ?? Guid.Empty
        }))
        {
            try
            {
                _logger.LogInformation(
                    "Received SowUploadedEvent for SOW {SowId} in Project {ProjectId}. Processing starting.", 
                    sowId, 
                    projectId);

                if (string.IsNullOrWhiteSpace(fileKey))
                {
                    _logger.LogError("SowUploadedEvent received with empty S3ObjectKey for SOW {SowId}. Message cannot be processed.", sowId);
                    // Depending on DLQ strategy, we might throw here to dead-letter, or just return to Ack (discard) invalid data.
                    // Assuming invalid data should be flagged manually, we'll log error and return to remove from queue.
                    return;
                }

                // Map message to internal application command
                var command = new ProcessSowCommand(
                    SowId: sowId,
                    FileKey: fileKey
                );

                // Dispatch to the Application Layer (Level 2 Handler)
                // The Handler is responsible for the business logic: 
                // Idempotency -> Status Update -> Download -> Sanitize -> Extract -> Persist
                var result = await _mediator.Send(command, context.CancellationToken);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully processed SOW {SowId}.", sowId);
                }
                else
                {
                    // If the handler returns a failure result, it implies the error was handled "gracefully" 
                    // (e.g., status updated to Failed in DB). We acknowledge the message to remove it from the queue.
                    _logger.LogWarning(
                        "SOW processing completed with failure status for {SowId}. Error: {ErrorMessage}", 
                        sowId, 
                        result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                // If an unhandled exception bubbles up (e.g., database connectivity loss that wasn't handled by inner retry policies),
                // we log it and re-throw. This allows MassTransit's retry policy to kick in (e.g., immediate retry, then delayed retry).
                // If retries are exhausted, MassTransit will move this to the _error queue (DLQ).
                _logger.LogError(ex, "Unhandled exception consuming SowUploadedEvent for SOW {SowId}.", sowId);
                throw;
            }
        }
    }
}

// Minimal definition of the contract to ensure compilation validity 
// in case the external EnterpriseMediator.Contracts assembly is not linked in this context context.
// In a real scenario, this would come from the referenced project.
namespace EnterpriseMediator.Contracts
{
    public record SowUploadedEvent(Guid SowId, Guid ProjectId, string S3ObjectKey);
}