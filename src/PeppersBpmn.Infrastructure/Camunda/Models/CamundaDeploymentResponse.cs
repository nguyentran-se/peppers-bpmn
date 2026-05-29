using System.Text.Json.Serialization;

namespace PeppersBpmn.Infrastructure.Camunda.Models;

public class CamundaDeploymentResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("deploymentTime")]
    public string DeploymentTime { get; set; } = string.Empty;

    [JsonPropertyName("deployedProcessDefinitions")]
    public Dictionary<string, CamundaProcessDefinitionResponse>? DeployedProcessDefinitions { get; set; }
}
