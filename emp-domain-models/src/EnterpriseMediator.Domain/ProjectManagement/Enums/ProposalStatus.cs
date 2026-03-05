namespace EnterpriseMediator.Domain.ProjectManagement.Enums
{
    /// <summary>
    /// Defines the states of a Vendor's Proposal for a Project.
    /// </summary>
    public enum ProposalStatus
    {
        /// <summary>
        /// Proposal has been submitted by the vendor but not yet reviewed.
        /// </summary>
        Submitted = 0,

        /// <summary>
        /// Proposal is currently under review by the administration team.
        /// </summary>
        InReview = 1,

        /// <summary>
        /// Proposal has been marked as a strong candidate for the project.
        /// </summary>
        Shortlisted = 2,

        /// <summary>
        /// Proposal has been accepted and the project awarded to this vendor.
        /// </summary>
        Accepted = 3,

        /// <summary>
        /// Proposal was not selected for the project.
        /// </summary>
        Rejected = 4,

        /// <summary>
        /// Vendor has withdrawn the proposal.
        /// </summary>
        Withdrawn = 5
    }
}