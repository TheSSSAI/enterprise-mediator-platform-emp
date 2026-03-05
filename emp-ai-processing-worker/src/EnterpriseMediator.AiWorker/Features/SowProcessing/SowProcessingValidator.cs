using EnterpriseMediator.AiWorker.Features.SowProcessing;
using FluentValidation;

namespace EnterpriseMediator.AiWorker.Features.SowProcessing
{
    /// <summary>
    /// Validates the ProcessSowCommand to ensure all required context data is present
    /// before attempting resource-intensive processing.
    /// </summary>
    public class SowProcessingValidator : AbstractValidator<ProcessSowCommand>
    {
        public SowProcessingValidator()
        {
            RuleFor(x => x.SowId)
                .NotEmpty()
                .WithMessage("SOW ID is required for processing.");

            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("Project ID is required to associate the SOW.");

            RuleFor(x => x.FileKey)
                .NotEmpty()
                .WithMessage("File Key is required to retrieve the document.")
                .Must(BeValidS3Key)
                .WithMessage("File Key contains invalid characters or format.");
        }

        private bool BeValidS3Key(string fileKey)
        {
            if (string.IsNullOrWhiteSpace(fileKey))
                return false;

            // Basic S3 key validation: ensure it doesn't start with a slash and has no illegal chars
            // This prevents basic path traversal logic errors in downstream storage adapters
            return !fileKey.StartsWith("/") && !fileKey.Contains("..");
        }
    }
}