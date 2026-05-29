using System.Text.Json.Serialization;

namespace PeppersBpmn.Infrastructure.Camunda.Models;

public class CamundaProcessInstanceResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("definitionId")]
    public string DefinitionId { get; set; } = string.Empty;

    [JsonPropertyName("businessKey")]
    public string? BusinessKey { get; set; }

    [JsonPropertyName("ended")]
    public bool Ended { get; set; }

    [JsonPropertyName("suspended")]
    public bool Suspended { get; set; }
}
