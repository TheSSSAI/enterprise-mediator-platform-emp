namespace EnterpriseMediator.Domain.ProjectManagement.Enums
{
    /// <summary>
    /// Defines the lifecycle states of a Project.
    /// </summary>
    public enum ProjectStatus
    {
        /// <summary>
        /// Project has been created but no SOW has been processed or brief approved.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Project Brief has been approved and distributed; awaiting proposals.
        /// </summary>
        Proposed = 1,

        /// <summary>
        /// A Vendor has been selected and the project has been awarded, but work/payment has not commenced.
        /// </summary>
        Awarded = 2,

        /// <summary>
        /// Initial payment received, work is in progress.
        /// </summary>
        Active = 3,

        /// <summary>
        /// SOW processing or Project Brief is under review by an admin.
        /// </summary>
        InReview = 4,

        /// <summary>
        /// All milestones delivered and project closed.
        /// </summary>
        Completed = 5,

        /// <summary>
        /// Project temporarily paused by an administrator.
        /// </summary>
        OnHold = 6,

        /// <summary>
        /// Project cancelled before completion.
        /// </summary>
        Cancelled = 7,

        /// <summary>
        /// Project is in dispute resolution.
        /// </summary>
        Disputed = 8
    }
}