namespace EnterpriseMediator.Domain.ProjectManagement.Enums
{
    /// <summary>
    /// Defines the processing states of a Statement of Work (SOW) document.
    /// </summary>
    public enum SowStatus
    {
        /// <summary>
        /// SOW uploaded but processing has not started.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// AI ingestion and sanitization in progress.
        /// </summary>
        Processing = 1,

        /// <summary>
        /// Processing complete, data extracted and sanitized.
        /// </summary>
        Processed = 2,

        /// <summary>
        /// Processing failed due to technical error or validation failure.
        /// </summary>
        Failed = 3
    }
}