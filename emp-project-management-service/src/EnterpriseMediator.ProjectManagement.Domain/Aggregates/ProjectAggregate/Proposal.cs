using System;

namespace EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate
{
    /// <summary>
    /// Represents the status of a vendor proposal in the evaluation lifecycle.
    /// </summary>
    public enum ProposalStatus
    {
        Submitted = 0,
        InReview = 1,
        Shortlisted = 2,
        Accepted = 3,
        Rejected = 4,
        Withdrawn = 5
    }

    /// <summary>
    /// Represents a formal bid submitted by a vendor for a specific project.
    /// Acts as an Entity within the Project Aggregate.
    /// </summary>
    public class Proposal
    {
        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid VendorId { get; private set; }
        
        /// <summary>
        /// The total cost proposed by the vendor.
        /// </summary>
        public decimal ProposedCost { get; private set; }
        
        /// <summary>
        /// ISO 4217 Currency Code (e.g., USD, EUR).
        /// </summary>
        public string Currency { get; private set; }
        
        /// <summary>
        /// Estimated timeline or duration provided by the vendor.
        /// </summary>
        public string Timeline { get; private set; }
        
        /// <summary>
        /// Description of key personnel assigned to the project.
        /// </summary>
        public string KeyPersonnel { get; private set; }
        
        /// <summary>
        /// URL to the uploaded proposal document (PDF/DOCX) in object storage.
        /// </summary>
        public string? ProposalDocumentUrl { get; private set; }
        
        public ProposalStatus Status { get; private set; }
        public DateTimeOffset SubmittedAt { get; private set; }
        
        /// <summary>
        /// Internal evaluation score (1-5) set by admins (US-053).
        /// </summary>
        public int? InternalScore { get; private set; }
        
        /// <summary>
        /// Internal categorization flag (e.g., "Top Contender", "Red Flag") (US-053).
        /// </summary>
        public string? InternalFlag { get; private set; }

        // EF Core binding constructor
        private Proposal() { }

        /// <summary>
        /// Creates a new Proposal instance. Enforces mandatory fields upon submission.
        /// </summary>
        public Proposal(
            Guid projectId, 
            Guid vendorId, 
            decimal proposedCost, 
            string currency, 
            string timeline, 
            string keyPersonnel, 
            string? proposalDocumentUrl)
        {
            if (projectId == Guid.Empty) throw new ArgumentException("Project ID cannot be empty.", nameof(projectId));
            if (vendorId == Guid.Empty) throw new ArgumentException("Vendor ID cannot be empty.", nameof(vendorId));
            if (proposedCost < 0) throw new ArgumentException("Proposed cost cannot be negative.", nameof(proposedCost));
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency code is required.", nameof(currency));
            if (string.IsNullOrWhiteSpace(timeline)) throw new ArgumentException("Timeline is required.", nameof(timeline));
            if (string.IsNullOrWhiteSpace(keyPersonnel)) throw new ArgumentException("Key personnel information is required.", nameof(keyPersonnel));

            Id = Guid.NewGuid();
            ProjectId = projectId;
            VendorId = vendorId;
            ProposedCost = proposedCost;
            Currency = currency.ToUpperInvariant();
            Timeline = timeline;
            KeyPersonnel = keyPersonnel;
            ProposalDocumentUrl = proposalDocumentUrl;
            
            // Default state upon creation
            Status = ProposalStatus.Submitted;
            SubmittedAt = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Moves the proposal to 'In Review' status.
        /// Typically triggered when an admin opens the proposal for detailed reading.
        /// </summary>
        public void MarkAsInReview()
        {
            if (Status == ProposalStatus.Submitted)
            {
                Status = ProposalStatus.InReview;
            }
        }

        /// <summary>
        /// Moves the proposal to 'Shortlisted'.
        /// Indicates the proposal is a strong candidate for acceptance.
        /// </summary>
        public void Shortlist()
        {
            if (Status == ProposalStatus.Rejected || Status == ProposalStatus.Withdrawn)
            {
                throw new InvalidOperationException($"Cannot shortlist a proposal that is {Status}.");
            }

            if (Status != ProposalStatus.Accepted) // If already accepted, shortlisting is a regression but technically allowed if un-accepting (edge case), but usually we just allow forward movement. Here we allow from Submitted/InReview.
            {
                Status = ProposalStatus.Shortlisted;
            }
        }

        /// <summary>
        /// Accepts the proposal, indicating it has won the bid.
        /// This should be orchestrated by the Project Aggregate Root to ensure only one proposal is accepted.
        /// </summary>
        public void Accept()
        {
            if (Status == ProposalStatus.Withdrawn)
            {
                throw new InvalidOperationException("Cannot accept a withdrawn proposal.");
            }
            
            Status = ProposalStatus.Accepted;
        }

        /// <summary>
        /// Rejects the proposal.
        /// </summary>
        public void Reject()
        {
            if (Status == ProposalStatus.Accepted)
            {
                throw new InvalidOperationException("Cannot reject a proposal that is already Accepted. Use a cancellation workflow instead.");
            }
            
            Status = ProposalStatus.Rejected;
        }

        /// <summary>
        /// Withdraws the proposal.
        /// Triggered by vendor action or if the vendor is deactivated (US-027).
        /// </summary>
        public void Withdraw()
        {
            if (Status == ProposalStatus.Accepted)
            {
                throw new InvalidOperationException("Cannot withdraw a proposal that has been Accepted and converted to a contract.");
            }

            Status = ProposalStatus.Withdrawn;
        }

        /// <summary>
        /// Updates the internal assessment details for the proposal (US-053).
        /// </summary>
        /// <param name="score">A score between 1 and 5.</param>
        /// <param name="flag">A descriptive flag (e.g., 'Top Contender').</param>
        public void UpdateAssessment(int? score, string? flag)
        {
            if (score.HasValue && (score.Value < 1 || score.Value > 5))
            {
                throw new ArgumentOutOfRangeException(nameof(score), "Internal score must be between 1 and 5.");
            }

            InternalScore = score;
            InternalFlag = flag;
        }
    }
}