using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.ProjectManagement.Enums;
using EnterpriseMediator.Domain.UserManagement.Aggregates;

namespace EnterpriseMediator.Domain.ProjectManagement.Aggregates;

/// <summary>
/// Represents a Statement of Work document uploaded for a project.
/// Manages the lifecycle of the document from upload through AI processing and sanitization.
/// </summary>
public class SowDocument : Entity<Guid>
{
    public ProjectId ProjectId { get; private set; }
    public string OriginalFileName { get; private set; }
    public string StorageKey { get; private set; }
    public string? SanitizedStorageKey { get; private set; }
    public SowStatus Status { get; private set; }
    public UserId UploadedBy { get; private set; }
    public DateTime UploadedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public string? ProcessingFailureReason { get; private set; }

    // EF Core constructor
    protected SowDocument() { }

    private SowDocument(
        Guid id,
        ProjectId projectId,
        string originalFileName,
        string storageKey,
        UserId uploadedBy,
        DateTime uploadedAt)
    {
        Id = id;
        ProjectId = projectId;
        OriginalFileName = originalFileName;
        StorageKey = storageKey;
        UploadedBy = uploadedBy;
        UploadedAt = uploadedAt;
        Status = SowStatus.Pending;
    }

    /// <summary>
    /// Creates a new SOW Document entity.
    /// </summary>
    public static SowDocument Create(
        ProjectId projectId,
        string originalFileName,
        string storageKey,
        UserId uploadedBy)
    {
        if (string.IsNullOrWhiteSpace(originalFileName))
            throw new BusinessRuleValidationException("Original filename is required.");
        
        if (string.IsNullOrWhiteSpace(storageKey))
            throw new BusinessRuleValidationException("Storage key is required.");

        return new SowDocument(
            Guid.NewGuid(),
            projectId,
            originalFileName,
            storageKey,
            uploadedBy,
            DateTime.UtcNow);
    }

    /// <summary>
    /// Transitions the SOW status to Processing.
    /// </summary>
    public void MarkAsProcessing()
    {
        if (Status != SowStatus.Pending && Status != SowStatus.Failed)
        {
            throw new BusinessRuleValidationException($"Cannot mark SOW as processing from status '{Status}'. Only Pending or Failed SOWs can be processed.");
        }

        Status = SowStatus.Processing;
        ProcessingFailureReason = null; // Clear previous errors on retry
    }

    /// <summary>
    /// Transitions the SOW status to Processed and records the sanitized file location.
    /// </summary>
    public void MarkAsProcessed(string sanitizedStorageKey)
    {
        if (Status != SowStatus.Processing)
        {
            throw new BusinessRuleValidationException($"Cannot mark SOW as processed from status '{Status}'. SOW must be in Processing state.");
        }

        if (string.IsNullOrWhiteSpace(sanitizedStorageKey))
        {
            throw new BusinessRuleValidationException("Sanitized storage key is required to mark SOW as processed.");
        }

        SanitizedStorageKey = sanitizedStorageKey;
        Status = SowStatus.Processed;
        ProcessedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Transitions the SOW status to Failed.
    /// </summary>
    public void MarkAsFailed(string reason)
    {
        // We allow failing from Pending if initial validation fails, or Processing if async work fails
        if (Status == SowStatus.Processed)
        {
            throw new BusinessRuleValidationException("Cannot mark an already processed SOW as failed.");
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new BusinessRuleValidationException("Failure reason is required.");
        }

        Status = SowStatus.Failed;
        ProcessingFailureReason = reason;
        ProcessedAt = DateTime.UtcNow; // Record when the failure decision was made
    }
}