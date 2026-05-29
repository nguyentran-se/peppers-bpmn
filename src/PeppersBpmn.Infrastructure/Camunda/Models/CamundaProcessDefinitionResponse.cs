using System.Text.Json.Serialization;

namespace PeppersBpmn.Infrastructure.Camunda.Models;

public class CamundaProcessDefinitionResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("resource")]
    public string? Resource { get; set; }

    [JsonPropertyName("deploymentId")]
    public string DeploymentId { get; set; } = string.Empty;
}
