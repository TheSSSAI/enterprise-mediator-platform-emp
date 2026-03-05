using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.ProjectManagement.Enums;
using EnterpriseMediator.Domain.ProjectManagement.Events;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.ProjectManagement.Aggregates;

/// <summary>
/// Represents a significant stage or deliverable in a project's lifecycle, often tied to a payment.
/// </summary>
public class Milestone : Entity<Guid>
{
    public ProjectId ProjectId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Amount { get; private set; }
    public DateTimeOffset DueDate { get; private set; }
    public MilestoneStatus Status { get; private set; }
    
    // Tracks if a payout has been initiated for this milestone to prevent duplicates
    public bool IsPayoutInitiated { get; private set; }

    // Required for EF Core
    protected Milestone() { }

    public Milestone(ProjectId projectId, string name, string description, Money amount, DateTimeOffset dueDate)
    {
        if (amount.Amount <= 0)
            throw new BusinessRuleValidationException("Milestone amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleValidationException("Milestone name is required.");

        if (dueDate < DateTimeOffset.UtcNow)
            throw new BusinessRuleValidationException("Milestone due date cannot be in the past.");

        Id = Guid.NewGuid();
        ProjectId = projectId;
        Name = name;
        Description = description;
        Amount = amount;
        DueDate = dueDate;
        Status = MilestoneStatus.Pending;
        IsPayoutInitiated = false;
    }

    /// <summary>
    /// Approves the milestone, indicating work is complete and accepted by the client.
    /// </summary>
    public void Approve()
    {
        if (Status != MilestoneStatus.Pending && Status != MilestoneStatus.Rejected)
            throw new BusinessRuleValidationException($"Cannot approve milestone in '{Status}' status.");

        Status = MilestoneStatus.Approved;
        
        // Raising event for downstream processes (e.g., Notifications, Payout readiness)
        AddDomainEvent(new MilestoneApprovedDomainEvent(Id, ProjectId, DateTimeOffset.UtcNow));
    }

    /// <summary>
    /// Rejects the milestone, usually requiring rework from the vendor.
    /// </summary>
    public void Reject(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new BusinessRuleValidationException("A reason is required to reject a milestone.");

        if (Status != MilestoneStatus.Pending && Status != MilestoneStatus.Approved)
            throw new BusinessRuleValidationException($"Cannot reject milestone in '{Status}' status.");

        Status = MilestoneStatus.Rejected;
        // In a real system, we might track the rejection reason in a separate history entity or audit log
    }

    /// <summary>
    /// Marks the milestone as paid. This typically happens after the Payout Aggregate completes its lifecycle.
    /// </summary>
    public void MarkAsPaid()
    {
        if (Status != MilestoneStatus.Approved)
            throw new BusinessRuleValidationException("Only approved milestones can be marked as paid.");

        Status = MilestoneStatus.Paid;
    }

    /// <summary>
    /// Flags that a payout process has started for this milestone.
    /// </summary>
    public void InitiatePayout()
    {
        if (Status != MilestoneStatus.Approved)
            throw new BusinessRuleValidationException("Cannot initiate payout for a milestone that is not approved.");
        
        if (IsPayoutInitiated)
            throw new BusinessRuleValidationException("Payout has already been initiated for this milestone.");

        IsPayoutInitiated = true;
    }

    public void UpdateDetails(string name, string description, DateTimeOffset dueDate)
    {
        if (Status == MilestoneStatus.Paid || IsPayoutInitiated)
            throw new BusinessRuleValidationException("Cannot update milestone details after payout processing has begun.");

        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessRuleValidationException("Milestone name cannot be empty.");

        Name = name;
        Description = description;
        DueDate = dueDate;
    }
}