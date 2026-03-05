namespace EnterpriseMediator.Domain.ProjectManagement.Enums
{
    /// <summary>
    /// Defines the lifecycle states of a Project Milestone.
    /// </summary>
    public enum MilestoneStatus
    {
        /// <summary>
        /// Milestone defined but work has not started or reached completion criteria.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Vendor has marked the milestone as complete and it is awaiting client approval.
        /// </summary>
        PendingApproval = 1,

        /// <summary>
        /// Client has approved the milestone deliverables.
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Client has rejected the milestone deliverables.
        /// </summary>
        Rejected = 3
    }
}