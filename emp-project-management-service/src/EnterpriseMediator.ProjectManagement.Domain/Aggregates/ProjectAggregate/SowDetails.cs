using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseMediator.ProjectManagement.Domain.Aggregates.ProjectAggregate
{
    /// <summary>
    /// A Value Object representing the structured data extracted from a Statement of Work (SOW) document.
    /// This data is typically populated by an AI processing worker and is used for vendor matching.
    /// </summary>
    public sealed record SowDetails
    {
        /// <summary>
        /// A high-level summary of the project scope.
        /// </summary>
        public string ScopeSummary { get; init; }

        /// <summary>
        /// List of specific deliverables expected from the project.
        /// </summary>
        public IReadOnlyList<string> Deliverables { get; init; }

        /// <summary>
        /// List of required skills identified in the SOW (e.g., "React", ".NET", "Cloud Architecture").
        /// </summary>
        public IReadOnlyList<string> RequiredSkills { get; init; }

        /// <summary>
        /// Specific technologies or platforms mentioned in the SOW.
        /// </summary>
        public IReadOnlyList<string> Technologies { get; init; }

        /// <summary>
        /// The extracted timeline or duration estimation text.
        /// </summary>
        public string EstimationTimeline { get; init; }

        /// <summary>
        /// Parameterless constructor for ORM materialization (EF Core).
        /// </summary>
        private SowDetails()
        {
            ScopeSummary = string.Empty;
            Deliverables = new List<string>();
            RequiredSkills = new List<string>();
            Technologies = new List<string>();
            EstimationTimeline = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SowDetails"/> value object.
        /// </summary>
        /// <param name="scopeSummary">The project scope summary.</param>
        /// <param name="deliverables">List of deliverables.</param>
        /// <param name="requiredSkills">List of required skills.</param>
        /// <param name="technologies">List of technologies.</param>
        /// <param name="estimationTimeline">Estimated timeline string.</param>
        public SowDetails(
            string scopeSummary, 
            IEnumerable<string> deliverables, 
            IEnumerable<string> requiredSkills, 
            IEnumerable<string> technologies, 
            string estimationTimeline)
        {
            ScopeSummary = scopeSummary ?? throw new ArgumentNullException(nameof(scopeSummary));
            Deliverables = deliverables?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
            RequiredSkills = requiredSkills?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
            Technologies = technologies?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
            EstimationTimeline = estimationTimeline ?? throw new ArgumentNullException(nameof(estimationTimeline));
        }

        /// <summary>
        /// Factory method to create an empty SOW detail object, typically used before AI processing.
        /// </summary>
        public static SowDetails CreateEmpty()
        {
            return new SowDetails(
                string.Empty,
                Enumerable.Empty<string>(),
                Enumerable.Empty<string>(),
                Enumerable.Empty<string>(),
                string.Empty
            );
        }

        /// <summary>
        /// Checks if the SOW details have been populated with meaningful data.
        /// </summary>
        public bool IsPopulated()
        {
            return !string.IsNullOrWhiteSpace(ScopeSummary) 
                   && (RequiredSkills.Any() || Deliverables.Any());
        }
    }
}