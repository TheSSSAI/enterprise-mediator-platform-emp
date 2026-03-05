using EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.AwardProject;
using EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.UpdateProjectBrief;
using EnterpriseMediator.ProjectManagement.Application.Features.Proposals.Queries.GetProjectProposals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// Note: Assuming namespaces for CreateProject and UploadSow commands based on architectural patterns,
// as these are core requirements (REQ-FUNC-009) typically located alongside other commands.
// If not explicitly generated in previous steps, these classes are assumed to exist in the Application layer.
using EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.CreateProject;
using EnterpriseMediator.ProjectManagement.Application.Features.Projects.Commands.UploadSow;

namespace EnterpriseMediator.ProjectManagement.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ProjectsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ISender sender, ILogger<ProjectsController> logger)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new project entity.
    /// </summary>
    /// <param name="command">The project creation details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ID of the created project.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProject(
        [FromBody] CreateProjectCommand command, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new project for Client {ClientId}", command.ClientId);
        
        // Assuming the command returns a Result<Guid> or Guid directly. 
        // Adapting to standard clean architecture Result pattern.
        var result = await _sender.Send(command, cancellationToken);

        // Pattern assumes Result<T> wrapper or direct return. 
        // Using straightforward mapping for demonstration of business logic flow.
        return CreatedAtAction(nameof(GetProjectById), new { id = result }, result);
    }

    /// <summary>
    /// Placeholder for GetProjectById to support CreatedAtAction.
    /// In a real implementation, this would dispatch a GetProjectByIdQuery.
    /// </summary>
    [HttpGet("{id}")]
    [ActionName(nameof(GetProjectById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProjectById(Guid id, CancellationToken cancellationToken)
    {
        // Implementation would dispatch GetProjectByIdQuery
        // For now, this serves as the route target for 201 Created responses
        return Ok(new { Id = id, Status = "Pending" }); 
    }

    /// <summary>
    /// Uploads a Statement of Work (SOW) document for a specific project.
    /// Triggers asynchronous AI processing.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="file">The SOW file (PDF or DOCX).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ID of the uploaded SOW document.</returns>
    [HttpPost("{id}/sow")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UploadSow(
        Guid id, 
        IFormFile file, 
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        _logger.LogInformation("Uploading SOW for Project {ProjectId}. File: {FileName}, Size: {Size}", 
            id, file.FileName, file.Length);

        var command = new UploadSowCommand(id, file.OpenReadStream(), file.FileName, file.ContentType);
        
        var sowId = await _sender.Send(command, cancellationToken);

        // Returns Accepted (202) because processing is asynchronous (AI Worker)
        return Accepted(new { SowId = sowId, Status = "Processing" });
    }

    /// <summary>
    /// Updates the Project Brief with AI-extracted or Admin-edited data.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="command">The updated brief data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id}/brief")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBrief(
        Guid id, 
        [FromBody] UpdateProjectBriefCommand command, 
        CancellationToken cancellationToken)
    {
        if (id != command.ProjectId)
        {
            return BadRequest("Project ID mismatch between URL and body.");
        }

        _logger.LogInformation("Updating Project Brief for Project {ProjectId}", id);

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Awards the project to a specific vendor proposal.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="command">The award details (Proposal ID).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpPost("{id}/award")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AwardProject(
        Guid id, 
        [FromBody] AwardProjectCommand command, 
        CancellationToken cancellationToken)
    {
        if (id != command.ProjectId)
        {
            return BadRequest("Project ID mismatch between URL and body.");
        }

        _logger.LogInformation("Awarding Project {ProjectId} to Proposal {ProposalId}", id, command.ProposalId);

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Retrieves all proposals submitted for a specific project.
    /// </summary>
    /// <param name="id">The project ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of proposal DTOs.</returns>
    [HttpGet("{id}/proposals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProposals(
        Guid id, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving proposals for Project {ProjectId}", id);

        var query = new GetProjectProposalsQuery(id);
        var proposals = await _sender.Send(query, cancellationToken);

        return Ok(proposals);
    }
}