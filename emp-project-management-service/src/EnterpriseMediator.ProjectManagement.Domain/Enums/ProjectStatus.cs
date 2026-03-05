namespace EnterpriseMediator.ProjectManagement.Domain.Enums;

/// <summary>
/// Represents the lifecycle states of a Project within the Enterprise Mediator Platform.
/// This enum drives the state machine logic for project workflows.
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    /// The initial state when a project is first created by an Admin.
    /// No SOW has been successfully processed yet.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// An SOW document has been uploaded and the AI analysis is currently running.
    /// </summary>
    Processing = 1,

    /// <summary>
    /// The AI processing failed. The admin must retry or upload a new SOW.
    /// </summary>
    Failed = 2,

    /// <summary>
    /// The SOW has been successfully processed and the extracted data is ready for Admin review.
    /// </summary>
    Processed = 3,

    /// <summary>
    /// The Project Brief has been approved by the Admin and the project is open for vendor proposals.
    /// Vendors are notified and can submit bids.
    /// </summary>
    Proposed = 4,

    /// <summary>
    /// A proposal has been accepted by the Admin.
    /// The project is awarded to a specific vendor, but work has not commenced (waiting for invoice payment).
    /// </summary>
    Awarded = 5,

    /// <summary>
    /// The client has paid the initial invoice. Funds are in escrow.
    /// The project is live and work is in progress.
    /// </summary>
    Active = 6,

    /// <summary>
    /// The project deliverables have been accepted and the project is closed.
    /// Final payouts are processed.
    /// </summary>
    Completed = 7,

    /// <summary>
    /// The project has been manually paused by an Admin.
    /// Workflows are suspended until resumed.
    /// </summary>
    OnHold = 8,

    /// <summary>
    /// The project has been cancelled before completion.
    /// This may trigger refund workflows.
    /// </summary>
    Cancelled = 9,

    /// <summary>
    /// The project is in a state of dispute regarding deliverables or payments.
    /// Requires Admin intervention to resolve funds.
    /// </summary>
    Disputed = 10
}