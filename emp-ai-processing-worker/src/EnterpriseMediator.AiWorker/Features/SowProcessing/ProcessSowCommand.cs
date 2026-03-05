using MediatR;

namespace EnterpriseMediator.AiWorker.Features.SowProcessing;

/// <summary>
/// Command to initiate the processing of an uploaded SOW document.
/// Contains all necessary context for the processing handler to locate, sanitize, and analyze the document.
/// </summary>
/// <param name="SowId">The unique identifier of the SOW record in the database.</param>
/// <param name="ProjectId">The identifier of the project associated with this SOW.</param>
/// <param name="FileKey">The object key (path) of the file in the storage system (S3).</param>
public record ProcessSowCommand(Guid SowId, Guid ProjectId, string FileKey) : IRequest<bool>
{
    /// <summary>
    /// Validates the command parameters.
    /// </summary>
    /// <returns>True if the command contains valid identifiers; otherwise, false.</returns>
    public bool IsValid()
    {
        return SowId != Guid.Empty && 
               ProjectId != Guid.Empty && 
               !string.IsNullOrWhiteSpace(FileKey);
    }
}