using System.Net.Http.Json;
using Emp.ApiGateway.Application.DTOs.Internal;
using Emp.ApiGateway.Application.Interfaces.Infrastructure;
using Emp.ApiGateway.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Emp.ApiGateway.Infrastructure.Services
{
    /// <summary>
    /// HTTP Client implementation for communicating with the Project Microservice.
    /// Utilizes HttpClientFactory and configured resilience policies.
    /// </summary>
    public class ProjectServiceClient : IProjectServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProjectServiceClient> _logger;
        private readonly ServiceUrls _serviceUrls;

        public ProjectServiceClient(
            HttpClient httpClient,
            IOptions<ServiceUrls> serviceUrls,
            ILogger<ProjectServiceClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceUrls = serviceUrls?.Value ?? throw new ArgumentNullException(nameof(serviceUrls));

            // Ensure base address is set if not configured via Factory
            if (_httpClient.BaseAddress == null && !string.IsNullOrEmpty(_serviceUrls.ProjectService))
            {
                _httpClient.BaseAddress = new Uri(_serviceUrls.ProjectService);
            }
        }

        public async Task<InternalProjectDto?> GetProjectDetailsAsync(Guid projectId, CancellationToken ct)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/projects/{projectId}", ct);

                if (response.IsSuccessStatusCode)
                {
                    var project = await response.Content.ReadFromJsonAsync<InternalProjectDto>(cancellationToken: ct);
                    return project;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogWarning("Project service returned 404 for ProjectId: {ProjectId}", projectId);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync(ct);
                _logger.LogError("Project service call failed. StatusCode: {StatusCode}, Content: {Content}", response.StatusCode, content);
                throw new HttpRequestException($"Project service call failed with status code {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching project details for ProjectId: {ProjectId}", projectId);
                throw;
            }
        }

        public async Task UploadSowAsync(Guid projectId, Stream fileStream, string fileName, CancellationToken ct)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                using var streamContent = new StreamContent(fileStream);
                
                content.Add(streamContent, "file", fileName);

                var response = await _httpClient.PostAsync($"api/v1/projects/{projectId}/sow", content, ct);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(ct);
                    _logger.LogError("Failed to upload SOW. StatusCode: {StatusCode}, Error: {Error}", response.StatusCode, errorContent);
                    throw new HttpRequestException($"SOW upload failed with status code {response.StatusCode}: {errorContent}");
                }

                _logger.LogInformation("Successfully uploaded SOW for ProjectId: {ProjectId}", projectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading SOW for ProjectId: {ProjectId}", projectId);
                throw;
            }
        }
    }
}