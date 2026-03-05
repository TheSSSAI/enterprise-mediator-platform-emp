using System.Net.Http.Json;
using Emp.ApiGateway.Application.DTOs.Internal;
using Emp.ApiGateway.Application.Interfaces.Infrastructure;
using Emp.ApiGateway.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Emp.ApiGateway.Infrastructure.Services
{
    /// <summary>
    /// HTTP Client implementation for communicating with the Financial Microservice.
    /// Utilizes HttpClientFactory and configured resilience policies.
    /// </summary>
    public class FinancialServiceClient : IFinancialServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FinancialServiceClient> _logger;
        private readonly ServiceUrls _serviceUrls;

        public FinancialServiceClient(
            HttpClient httpClient,
            IOptions<ServiceUrls> serviceUrls,
            ILogger<FinancialServiceClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceUrls = serviceUrls?.Value ?? throw new ArgumentNullException(nameof(serviceUrls));

            if (_httpClient.BaseAddress == null && !string.IsNullOrEmpty(_serviceUrls.FinancialService))
            {
                _httpClient.BaseAddress = new Uri(_serviceUrls.FinancialService);
            }
        }

        public async Task<FinancialSummaryDto?> GetProjectFinancialSummaryAsync(Guid projectId, CancellationToken ct)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/projects/{projectId}/financials", ct);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FinancialSummaryDto>(cancellationToken: ct);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogWarning("Financial service returned 404 for ProjectId: {ProjectId}", projectId);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync(ct);
                _logger.LogError("Financial service call failed. StatusCode: {StatusCode}, Content: {Content}", response.StatusCode, content);
                
                // Depending on resilience strategy, we might return null here to degrade gracefully,
                // or throw to fail the whole request. This implementation throws to alert of system issues.
                throw new HttpRequestException($"Financial service call failed with status code {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching financial summary for ProjectId: {ProjectId}", projectId);
                throw;
            }
        }
    }
}