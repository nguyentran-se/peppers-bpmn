using System.Text.Json.Serialization;

namespace PeppersBpmn.Infrastructure.Camunda.Models;

public class CamundaTaskResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("assignee")]
    public string? Assignee { get; set; }

    [JsonPropertyName("processInstanceId")]
    public string ProcessInstanceId { get; set; } = string.Empty;

    [JsonPropertyName("processDefinitionId")]
    public string ProcessDefinitionId { get; set; } = string.Empty;

    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    [JsonPropertyName("due")]
    public DateTime? Due { get; set; }

    [JsonPropertyName("taskDefinitionKey")]
    public string? TaskDefinitionKey { get; set; }
}
