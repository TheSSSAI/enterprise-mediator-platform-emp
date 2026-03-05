using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMediator.Domain.Common;
using EnterpriseMediator.Domain.Common.Exceptions;
using EnterpriseMediator.Domain.Financials.Enums;
using EnterpriseMediator.Domain.ProjectManagement.Enums;
using EnterpriseMediator.Domain.ProjectManagement.Events;
using EnterpriseMediator.Domain.Shared.ValueObjects;
using EnterpriseMediator.Domain.VendorManagement.Aggregates;
using EnterpriseMediator.Domain.ClientManagement.Aggregates;

namespace EnterpriseMediator.Domain.ProjectManagement.Aggregates
{
    public class Project : AggregateRoot<ProjectId>
    {
        private readonly List<Proposal> _proposals = new();
        private readonly List<Milestone> _milestones = new();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ClientId ClientId { get; private set; }
        public ProjectStatus Status { get; private set; }
        public Money? Budget { get; private set; }
        
        public SowDocument? SowDocument { get; private set; }
        public ProjectBrief? Brief { get; private set; }
        
        public VendorId? SelectedVendorId { get; private set; }
        public ProposalId? AcceptedProposalId { get; private set; }

        public IReadOnlyCollection<Proposal> Proposals => _proposals.AsReadOnly();
        public IReadOnlyCollection<Milestone> Milestones => _milestones.AsReadOnly();

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }

        // EF Core Constructor
        #pragma warning disable CS8618
        private Project() { }
        #pragma warning restore CS8618

        private Project(ProjectId id, string name, string description, ClientId clientId, Money? budget)
        {
            Id = id;
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new BusinessRuleValidationException("Project name is required.");
            Description = description;
            ClientId = clientId ?? throw new BusinessRuleValidationException("Client ID is required.");
            Budget = budget;
            Status = ProjectStatus.Pending;
            CreatedAt = DateTimeOffset.UtcNow;

            AddDomainEvent(new ProjectCreatedDomainEvent(Id, ClientId, CreatedAt));
        }

        public static Project Create(string name, string description, ClientId clientId, Money? budget = null)
        {
            return new Project(new ProjectId(Guid.NewGuid()), name, description, clientId, budget);
        }

        public void AttachSow(SowDocument sow)
        {
            if (sow == null) throw new ArgumentNullException(nameof(sow));
            
            if (Status != ProjectStatus.Pending)
            {
                throw new BusinessRuleValidationException("SOW can only be uploaded when project status is Pending.");
            }

            if (SowDocument != null && SowDocument.Status == SowStatus.Processed)
            {
                throw new BusinessRuleValidationException("Cannot replace an SOW that has already been processed.");
            }

            SowDocument = sow;
            AddDomainEvent(new SowUploadedDomainEvent(Id, sow.Id));
        }

        public void DefineBrief(ProjectBrief brief)
        {
            if (brief == null) throw new ArgumentNullException(nameof(brief));
            
            // Brief implies we are moving towards distribution, usually SOW processed
            if (Status != ProjectStatus.Pending && Status != ProjectStatus.Proposed)
            {
                throw new BusinessRuleValidationException("Project Brief can only be defined in Pending or Proposed states.");
            }

            Brief = brief;
            // Transition to Proposed typically happens when distributed, but defining it is a prereq
        }

        public void DistributeBrief()
        {
            if (Brief == null)
            {
                throw new BusinessRuleValidationException("Cannot distribute project without a defined Project Brief.");
            }

            if (Status != ProjectStatus.Pending)
            {
                throw new BusinessRuleValidationException("Project must be Pending to distribute brief.");
            }

            Status = ProjectStatus.Proposed;
            AddDomainEvent(new ProjectStatusChangedDomainEvent(Id, ProjectStatus.Pending, ProjectStatus.Proposed));
        }

        public void AddProposal(Proposal proposal)
        {
            if (proposal == null) throw new ArgumentNullException(nameof(proposal));

            if (Status != ProjectStatus.Proposed)
            {
                throw new BusinessRuleValidationException("Proposals can only be accepted when project is in Proposed status.");
            }

            if (_proposals.Any(p => p.VendorId == proposal.VendorId))
            {
                throw new BusinessRuleValidationException("Vendor has already submitted a proposal for this project.");
            }

            _proposals.Add(proposal);
        }

        public void AwardToVendor(VendorId vendorId, ProposalId proposalId)
        {
            if (Status != ProjectStatus.Proposed)
            {
                throw new BusinessRuleValidationException("Can only award project when in Proposed status.");
            }

            var proposal = _proposals.FirstOrDefault(p => p.Id == proposalId);
            if (proposal == null)
            {
                throw new BusinessRuleValidationException("Proposal not found for this project.");
            }

            if (proposal.VendorId != vendorId)
            {
                throw new BusinessRuleValidationException("Proposal does not belong to the specified vendor.");
            }

            SelectedVendorId = vendorId;
            AcceptedProposalId = proposalId;
            Status = ProjectStatus.Awarded;

            // Reject all other proposals
            foreach (var prop in _proposals.Where(p => p.Id != proposalId))
            {
                prop.Reject("Project awarded to another vendor.");
            }

            proposal.Accept();

            AddDomainEvent(new ProjectAwardedDomainEvent(Id, vendorId, proposalId));
            AddDomainEvent(new ProjectStatusChangedDomainEvent(Id, ProjectStatus.Proposed, ProjectStatus.Awarded));
        }

        public void ActivateProject()
        {
            if (Status != ProjectStatus.Awarded)
            {
                throw new BusinessRuleValidationException("Project can only be activated from Awarded status.");
            }

            Status = ProjectStatus.Active;
            StartDate = DateTimeOffset.UtcNow;
            AddDomainEvent(new ProjectStatusChangedDomainEvent(Id, ProjectStatus.Awarded, ProjectStatus.Active));
        }

        public void AddMilestone(Milestone milestone)
        {
            if (milestone == null) throw new ArgumentNullException(nameof(milestone));
            
            // Milestones can be defined during award or active phase usually
            if (Status == ProjectStatus.Completed || Status == ProjectStatus.Cancelled)
            {
                throw new BusinessRuleValidationException("Cannot add milestones to a closed project.");
            }

            _milestones.Add(milestone);
        }

        public void CompleteProject()
        {
            if (Status != ProjectStatus.Active)
            {
                throw new BusinessRuleValidationException("Only active projects can be completed.");
            }

            if (_milestones.Any(m => m.Status != MilestoneStatus.Approved))
            {
                throw new BusinessRuleValidationException("All milestones must be approved before completing the project.");
            }

            Status = ProjectStatus.Completed;
            EndDate = DateTimeOffset.UtcNow;
            AddDomainEvent(new ProjectStatusChangedDomainEvent(Id, ProjectStatus.Active, ProjectStatus.Completed));
        }

        public void CancelProject(string reason)
        {
            if (Status == ProjectStatus.Completed || Status == ProjectStatus.Cancelled)
            {
                throw new BusinessRuleValidationException("Project is already closed.");
            }

            var oldStatus = Status;
            Status = ProjectStatus.Cancelled;
            AddDomainEvent(new ProjectStatusChangedDomainEvent(Id, oldStatus, ProjectStatus.Cancelled));
        }

        public void HoldProject()
        {
            if (Status != ProjectStatus.Active)
            {
                throw new BusinessRuleValidationException("Only active projects can be put on hold.");
            }

            Status = ProjectStatus.OnHold;
            AddDomainEvent(new ProjectStatusChangedDomainEvent(Id, ProjectStatus.Active, ProjectStatus.OnHold));
        }

        public void ResumeProject()
        {
            if (Status != ProjectStatus.OnHold)
            {
                throw new BusinessRuleValidationException("Only projects on hold can be resumed.");
            }

            Status = ProjectStatus.Active;
            AddDomainEvent(new ProjectStatusChangedDomainEvent(Id, ProjectStatus.OnHold, ProjectStatus.Active));
        }
    }
}