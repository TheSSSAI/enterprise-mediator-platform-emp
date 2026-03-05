using System.Net.Mime;
using Emp.ApiGateway.Application.DTOs.Public;
using Emp.ApiGateway.Application.Features.Projects.Commands;
using Emp.ApiGateway.Application.Features.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emp.ApiGateway.Web.Controllers;

/// <summary>
/// Public API Controller for Project management operations.
/// Acts as a BFF entry point orchestrating calls to the underlying Project Microservice.
/// </summary>
[ApiController]
[Route("api/v1/projects")]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
public class ProjectsController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ISender mediator, ILogger<ProjectsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new project in the system.
    /// </summary>
    /// <param name="request">The project creation details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The unique identifier of the created project.</returns>
    /// <response code="201">Project successfully created.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">Unauthorized access.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> CreateProject(
        [FromBody] CreateProjectRequest request,
        CancellationToken ct)
    {
        _logger.LogInformation("Received request to create project: {ProjectName}", request.Name);

        var command = new CreateProjectCommand(request.Name, request.Description);
        var projectId = await _mediator.Send(command, ct);

        _logger.LogInformation("Project created successfully with ID: {ProjectId}", projectId);

        // Following REST conventions, we return 201 Created with a Location header
        return CreatedAtAction(nameof(GetProjectDashboard), new { projectId }, projectId);
    }

    /// <summary>
    /// Retrieves the aggregated dashboard view for a specific project.
    /// This is a BFF-pattern endpoint that aggregates data from Project and Financial services.
    /// </summary>
    /// <param name="projectId">The unique identifier of the project.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Aggregated project dashboard data.</returns>
    /// <response code="200">Returns the dashboard data.</response>
    /// <response code="404">Project not found.</response>
    [HttpGet("{projectId:guid}/dashboard")]
    [ProducesResponseType(typeof(ProjectDashboardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectDashboardResponse>> GetProjectDashboard(
        [FromRoute] Guid projectId,
        CancellationToken ct)
    {
        _logger.LogDebug("Retrieving dashboard for project: {ProjectId}", projectId);

        var query = new GetProjectDashboardQuery(projectId);
        var result = await _mediator.Send(query, ct);

        if (result == null)
        {
            _logger.LogWarning("Project dashboard not found for ID: {ProjectId}", projectId);
            return NotFound($"Project with ID {projectId} not found.");
        }

        return Ok(result);
    }

    /// <summary>
    /// Uploads a Statement of Work (SOW) document for a project.
    /// The file is streamed to storage and triggers an asynchronous AI processing workflow.
    /// </summary>
    /// <param name="projectId">The project identifier.</param>
    /// <param name="file">The SOW document (PDF or DOCX).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Accepted status indicating processing has started.</returns>
    /// <response code="202">File accepted for processing.</response>
    /// <response code="400">Invalid file format or size.</response>
    [HttpPost("{projectId:guid}/sow")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadSow(
        [FromRoute] Guid projectId,
        IFormFile file,
        CancellationToken ct)
    {
        if (file == null || file.Length == 0)
        {
            _logger.LogWarning("Upload SOW attempt with empty file for project {ProjectId}", projectId);
            return BadRequest("No file uploaded.");
        }

        // Basic validation - 10MB limit (example policy)
        const long maxFileSize = 10 * 1024 * 1024;
        if (file.Length > maxFileSize)
        {
            return BadRequest("File size exceeds the maximum allowed limit of 10MB.");
        }

        var allowedExtensions = new[] { ".pdf", ".docx", ".doc" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest("Invalid file type. Only PDF and Word documents are allowed.");
        }

        _logger.LogInformation(
            "Uploading SOW for project {ProjectId}. FileName: {FileName}, Size: {Size}", 
            projectId, 
            file.FileName, 
            file.Length);

        // We process the stream directly to avoid memory buffering issues with large files
        using var stream = file.OpenReadStream();
        
        var command = new UploadSowCommand(projectId, stream, file.FileName, file.ContentType);
        await _mediator.Send(command, ct);

        // Return 202 Accepted because the AI processing is asynchronous
        return Accepted(new { message = "SOW uploaded successfully and queued for processing." });
    }
}