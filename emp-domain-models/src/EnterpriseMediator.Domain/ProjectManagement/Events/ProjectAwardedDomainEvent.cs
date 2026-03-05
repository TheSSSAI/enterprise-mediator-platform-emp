using System;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.ProjectManagement.Aggregates;
using EnterpriseMediator.Domain.VendorManagement.Aggregates;
using EnterpriseMediator.Domain.Shared.ValueObjects;

namespace EnterpriseMediator.Domain.ProjectManagement.Events;

/// <summary>
/// Domain event raised when a project is officially awarded to a vendor based on a proposal.
/// </summary>
/// <param name="ProjectId">The unique identifier of the project.</param>
/// <param name="VendorId">The vendor who was awarded the project.</param>
/// <param name="ProposalId">The specific proposal that was accepted.</param>
/// <param name="AwardAmount">The financial value of the award (Proposal Cost).</param>
/// <param name="AwardedBy">The ID of the System Admin who authorized the award.</param>
/// <param name="OccurredOn">Timestamp of the event.</param>
public record ProjectAwardedDomainEvent(
    ProjectId ProjectId,
    VendorId VendorId,
    Guid ProposalId,
    Money AwardAmount,
    Guid AwardedBy,
    DateTime OccurredOn
) : IDomainEvent;