using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriseMediator.ProjectManagement.Domain.Enums;
using EnterpriseMediator.ProjectManagement.Domain.Events;

namespace EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate
{
    /// <summary>
    /// Represents the Project Aggregate Root.
    /// Manages the lifecycle of a project, including SOW processing, proposal management, and financial configuration.
    /// </summary>
    public class Project
    {
        private readonly List<Proposal> _proposals = new();
        private readonly List<ProjectPayoutRule> _payoutRules = new();
        private readonly List<object> _domainEvents = new();

        /// <summary>
        /// Protected constructor for ORM serialization.
        /// </summary>
        protected Project() { }

        /// <summary>
        /// Private constructor to enforce factory creation.
        /// </summary>
        private Project(Guid id, Guid clientId, string name, string description)
        {
            if (id == Guid.Empty) throw new ArgumentException("Project ID cannot be empty.", nameof(id));
            if (clientId == Guid.Empty) throw new ArgumentException("Client ID cannot be empty.", nameof(clientId));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Project name cannot be empty.", nameof(name));

            Id = id;
            ClientId = clientId;
            Name = name;
            Description = description;
            Status = ProjectStatus.Pending;
            CreatedOn = DateTime.UtcNow;
        }

        // Core Identity & Audit
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? CompletedOn { get; private set; }
        public ProjectStatus Status { get; private set; }

        // SOW Data (Value Object)
        public SowDetails? SowDetails { get; private set; }

        // Financial Configuration
        public decimal? FixedMargin { get; private set; }
        public decimal? PercentageMargin { get; private set; }
        public bool IsMarginConfigured => FixedMargin.HasValue || PercentageMargin.HasValue;

        // Relationships
        public IReadOnlyCollection<Proposal> Proposals => _proposals.AsReadOnly();
        public IReadOnlyCollection<ProjectPayoutRule> PayoutRules => _payoutRules.AsReadOnly();
        
        // Domain Events
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Factory method to create a new Project.
        /// </summary>
        /// <param name="clientId">The ID of the client owner.</param>
        /// <param name="name">The project name.</param>
        /// <param name="description">Optional project description.</param>
        /// <returns>A new Project instance.</returns>
        public static Project Create(Guid clientId, string name, string description)
        {
            var project = new Project(Guid.NewGuid(), clientId, name, description);
            // Example event: project.AddDomainEvent(new ProjectCreatedDomainEvent(project.Id, clientId));
            return project;
        }

        /// <summary>
        /// Associates a processed SOW with the project.
        /// </summary>
        /// <param name="sowDetails">The extracted SOW details.</param>
        public void AttachSow(SowDetails sowDetails)
        {
            if (Status != ProjectStatus.Pending && Status != ProjectStatus.Processing)
            {
                throw new InvalidOperationException($"Cannot attach SOW when project status is {Status}.");
            }

            SowDetails = sowDetails ?? throw new ArgumentNullException(nameof(sowDetails));
            Status = ProjectStatus.Processed;
        }

        /// <summary>
        /// Updates the SOW details during the human-in-the-loop review process.
        /// </summary>
        /// <param name="updatedSowDetails">The reviewed and edited SOW details.</param>
        public void UpdateBrief(SowDetails updatedSowDetails)
        {
            if (Status != ProjectStatus.Processed && Status != ProjectStatus.BriefApproved)
            {
                throw new InvalidOperationException($"Cannot update brief data when project status is {Status}. Brief must be in Processed or Approved state before distribution.");
            }

            SowDetails = updatedSowDetails ?? throw new ArgumentNullException(nameof(updatedSowDetails));
        }

        /// <summary>
        /// Approves the Project Brief, locking it for distribution.
        /// </summary>
        public void ApproveBrief()
        {
            if (Status != ProjectStatus.Processed)
            {
                throw new InvalidOperationException($"Cannot approve brief. Current status is {Status}, expected Processed.");
            }

            if (SowDetails == null)
            {
                throw new InvalidOperationException("Cannot approve brief. SOW Details are missing.");
            }

            Status = ProjectStatus.BriefApproved;
        }

        /// <summary>
        /// Distributes the brief to vendors, opening the proposal window.
        /// </summary>
        public void DistributeBrief()
        {
            if (Status != ProjectStatus.BriefApproved)
            {
                throw new InvalidOperationException($"Cannot distribute brief. Current status is {Status}, expected BriefApproved.");
            }

            Status = ProjectStatus.Proposed;
        }

        /// <summary>
        /// Adds a vendor proposal to the project.
        /// </summary>
        /// <param name="proposal">The proposal to add.</param>
        public void AddProposal(Proposal proposal)
        {
            if (Status != ProjectStatus.Proposed)
            {
                throw new InvalidOperationException($"Cannot add proposal. Project is not in Proposed state (Current: {Status}).");
            }

            if (_proposals.Any(p => p.VendorId == proposal.VendorId))
            {
                throw new InvalidOperationException("This vendor has already submitted a proposal for this project.");
            }

            _proposals.Add(proposal);
        }

        /// <summary>
        /// Awards the project to a specific proposal.
        /// </summary>
        /// <param name="proposalId">The ID of the winning proposal.</param>
        public void AwardTo(Guid proposalId)
        {
            if (Status != ProjectStatus.Proposed)
            {
                throw new InvalidOperationException($"Cannot award project. Current status is {Status}, expected Proposed.");
            }

            var winningProposal = _proposals.FirstOrDefault(p => p.Id == proposalId);
            if (winningProposal == null)
            {
                throw new ArgumentException("Proposal not found in this project.", nameof(proposalId));
            }

            // Accept the winning proposal
            winningProposal.Accept();

            // Reject all other proposals
            foreach (var proposal in _proposals.Where(p => p.Id != proposalId))
            {
                proposal.Reject();
            }

            Status = ProjectStatus.Awarded;

            // Raise domain event to trigger downstream processes (e.g. Invoicing)
            AddDomainEvent(new ProjectAwardedDomainEvent(Id, winningProposal.VendorId, winningProposal.Id, winningProposal.Cost));
        }

        /// <summary>
        /// Configures the financial margin for the project.
        /// </summary>
        /// <param name="fixedMargin">Fixed fee amount.</param>
        /// <param name="percentageMargin">Percentage markup.</param>
        public void ConfigureFinancials(decimal? fixedMargin, decimal? percentageMargin)
        {
            if (Status == ProjectStatus.Active || Status == ProjectStatus.Completed)
            {
                throw new InvalidOperationException("Cannot modify financials for an active or completed project.");
            }

            if (fixedMargin.HasValue && percentageMargin.HasValue)
            {
                throw new ArgumentException("Cannot configure both Fixed Margin and Percentage Margin. Choose one.");
            }

            if (fixedMargin.HasValue && fixedMargin.Value < 0)
            {
                throw new ArgumentException("Fixed margin cannot be negative.");
            }

            if (percentageMargin.HasValue && (percentageMargin.Value < 0 || percentageMargin.Value > 100))
            {
                throw new ArgumentException("Percentage margin must be between 0 and 100.");
            }

            FixedMargin = fixedMargin;
            PercentageMargin = percentageMargin;
        }

        /// <summary>
        /// Activates the project (usually triggered after invoice payment).
        /// </summary>
        public void Activate()
        {
            if (Status != ProjectStatus.Awarded)
            {
                throw new InvalidOperationException($"Cannot activate project. Current status is {Status}, expected Awarded.");
            }

            Status = ProjectStatus.Active;
        }

        /// <summary>
        /// Marks the project as completed.
        /// </summary>
        public void Complete()
        {
            if (Status != ProjectStatus.Active)
            {
                throw new InvalidOperationException($"Cannot complete project. Current status is {Status}, expected Active.");
            }

            Status = ProjectStatus.Completed;
            CompletedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Cancels the project.
        /// </summary>
        public void Cancel()
        {
            if (Status == ProjectStatus.Completed)
            {
                throw new InvalidOperationException("Cannot cancel a completed project.");
            }

            Status = ProjectStatus.Cancelled;
            // Additional logic for withdrawing pending proposals could be added here if needed
        }

        /// <summary>
        /// Adds a payout rule to the project.
        /// </summary>
        /// <param name="rule">The payout rule to add.</param>
        public void AddPayoutRule(ProjectPayoutRule rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            
            // Basic validation to ensure rules don't exceed 100% could be done here or in a service
            _payoutRules.Add(rule);
        }

        /// <summary>
        /// Adds a domain event to the collection.
        /// </summary>
        private void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clears domain events. Called by infrastructure after dispatch.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}