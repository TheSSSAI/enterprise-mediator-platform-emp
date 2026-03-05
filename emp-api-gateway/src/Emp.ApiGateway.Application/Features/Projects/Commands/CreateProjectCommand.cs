using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Emp.ApiGateway.Application.Features.Projects.Commands
{
    /// <summary>
    /// Command to initiate the creation of a new project.
    /// </summary>
    public record CreateProjectCommand : IRequest<Guid>
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        [Required(ErrorMessage = "Project name is required")]
        [MaxLength(100, ErrorMessage = "Project name cannot exceed 100 characters")]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// A brief description of the project scope.
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// The ID of the client associated with this project.
        /// </summary>
        [Required(ErrorMessage = "Client ID is required")]
        public Guid ClientId { get; init; }

        /// <summary>
        /// The anticipated start date of the project.
        /// </summary>
        public DateTime? StartDate { get; init; }

        /// <summary>
        /// The anticipated end date of the project.
        /// </summary>
        public DateTime? EndDate { get; init; }
    }
}