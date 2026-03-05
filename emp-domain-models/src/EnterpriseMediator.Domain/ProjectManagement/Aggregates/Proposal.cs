using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.ProjectManagement.Enums;
using EnterpriseMediator.Domain.Shared.ValueObjects;
using EnterpriseMediator.Domain.VendorManagement.Aggregates;

namespace EnterpriseMediator.Domain.ProjectManagement.Aggregates;

/// <summary>
/// Represents a bid submitted by a Vendor for a specific Project.
/// </summary>
public class Proposal : Entity<Guid>
{
    public ProjectId ProjectId { get; private set; }
    public VendorId VendorId { get; private set; }
    public Money ProposedCost { get; private set; }
    public string TimelineEstimate { get; private set; }
    public string CoverLetter { get; private set; }
    public string KeyPersonnel { get; private set; }
    public DateTimeOffset SubmittedAt { get; private set; }
    public ProposalStatus Status { get; private set; }

    // Internal evaluation fields (US-053)
    public int? InternalScore { get; private set; } // 1-5 rating
    public string? InternalFlag { get; private set; } // e.g., "Top Contender", "Red Flag"

    // EF Core constructor
    protected Proposal() { }

    public Proposal(ProjectId projectId, VendorId vendorId, Money proposedCost, string timelineEstimate, string coverLetter, string keyPersonnel)
    {
        if (proposedCost.Amount <= 0)
            throw new BusinessRuleValidationException("Proposal cost must be greater than zero.");

        if (string.IsNullOrWhiteSpace(timelineEstimate))
            throw new BusinessRuleValidationException("Timeline estimate is required.");

        Id = Guid.NewGuid();
        ProjectId = projectId;
        VendorId = vendorId;
        ProposedCost = proposedCost;
        TimelineEstimate = timelineEstimate;
        CoverLetter = coverLetter;
        KeyPersonnel = keyPersonnel;
        Status = ProposalStatus.Submitted;
        SubmittedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Moves the proposal to 'In Review' status.
    /// </summary>
    public void MarkInReview()
    {
        if (Status != ProposalStatus.Submitted)
            throw new BusinessRuleValidationException($"Cannot mark proposal as In Review from status '{Status}'.");

        Status = ProposalStatus.InReview;
    }

    /// <summary>
    /// Moves the proposal to 'Shortlisted' status.
    /// </summary>
    public void Shortlist()
    {
        if (Status != ProposalStatus.Submitted && Status != ProposalStatus.InReview)
            throw new BusinessRuleValidationException($"Cannot shortlist proposal from status '{Status}'.");

        Status = ProposalStatus.Shortlisted;
    }

    /// <summary>
    /// Accepts the proposal, indicating this vendor has won the project.
    /// Note: This logic typically triggers the rejection of all other proposals in the domain service/handler.
    /// </summary>
    public void Accept()
    {
        if (Status == ProposalStatus.Rejected || Status == ProposalStatus.Withdrawn)
            throw new BusinessRuleValidationException($"Cannot accept a proposal that is '{Status}'.");

        Status = ProposalStatus.Accepted;
    }

    /// <summary>
    /// Rejects the proposal.
    /// </summary>
    public void Reject()
    {
        if (Status == ProposalStatus.Accepted)
            throw new BusinessRuleValidationException("Cannot reject a proposal that has already been accepted.");

        Status = ProposalStatus.Rejected;
    }

    /// <summary>
    /// Allows the vendor to withdraw their proposal before it is accepted/rejected.
    /// </summary>
    public void Withdraw()
    {
        if (Status == ProposalStatus.Accepted || Status == ProposalStatus.Rejected)
            throw new BusinessRuleValidationException("Cannot withdraw a finalized proposal.");

        Status = ProposalStatus.Withdrawn;
    }

    /// <summary>
    /// Sets internal evaluation metrics (US-053).
    /// </summary>
    public void SetInternalAssessment(int? score, string? flag)
    {
        if (score.HasValue && (score < 1 || score > 5))
            throw new BusinessRuleValidationException("Internal score must be between 1 and 5.");

        InternalScore = score;
        InternalFlag = flag;
    }
}