using System;
using System.IO;
using MediatR;

namespace Emp.ApiGateway.Application.Features.Projects.Commands
{
    /// <summary>
    /// Command to upload a Statement of Work (SOW) document for processing.
    /// </summary>
    public record UploadSowCommand : IRequest<Unit>, IDisposable
    {
        /// <summary>
        /// The ID of the project the SOW belongs to.
        /// </summary>
        public Guid ProjectId { get; }

        /// <summary>
        /// The stream containing the file content.
        /// </summary>
        public Stream FileStream { get; }

        /// <summary>
        /// The original name of the uploaded file.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The content type (MIME type) of the file.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Initializes a new instance of the UploadSowCommand.
        /// </summary>
        /// <param name="projectId">Project Identifier.</param>
        /// <param name="fileStream">Content Stream.</param>
        /// <param name="fileName">Original Filename.</param>
        /// <param name="contentType">MIME Type.</param>
        public UploadSowCommand(Guid projectId, Stream fileStream, string fileName, string contentType)
        {
            ProjectId = projectId;
            FileStream = fileStream ?? throw new ArgumentNullException(nameof(fileStream));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }

        /// <summary>
        /// Disposes the underlying file stream.
        /// </summary>
        public void Dispose()
        {
            FileStream?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}