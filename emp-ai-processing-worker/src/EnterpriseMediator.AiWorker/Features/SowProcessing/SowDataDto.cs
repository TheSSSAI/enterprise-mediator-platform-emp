using System.Text.Json.Serialization;

namespace EnterpriseMediator.AiWorker.Features.SowProcessing;

/// <summary>
/// Represents the structured data extracted from a raw Statement of Work (SOW) document by the AI service.
/// This structure matches the JSON schema expected from the LLM extraction process.
/// </summary>
public class SowDataDto
{
    /// <summary>
    /// A concise summary of the project scope defined in the SOW.
    /// </summary>
    [JsonPropertyName("scope_summary")]
    public string ScopeSummary { get; set; } = string.Empty;

    /// <summary>
    /// A list of technical skills, technologies, or expertise required for the project.
    /// </summary>
    [JsonPropertyName("required_skills")]
    public List<string> RequiredSkills { get; set; } = new();

    /// <summary>
    /// A list of specific outputs or deliverables defined in the SOW.
    /// </summary>
    [JsonPropertyName("deliverables")]
    public List<string> Deliverables { get; set; } = new();

    /// <summary>
    /// The extracted project timeline or duration description.
    /// </summary>
    [JsonPropertyName("timeline")]
    public string? Timeline { get; set; }

    /// <summary>
    /// Validates the integrity of the extracted data.
    /// </summary>
    /// <returns>True if the data contains minimum viable information; otherwise, false.</returns>
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(ScopeSummary) && 
               RequiredSkills != null && 
               RequiredSkills.Count > 0;
    }
}