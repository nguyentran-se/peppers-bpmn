using PeppersBpmn.Domain.ValueObjects;

namespace PeppersBpmn.Domain.Entities;

public class ProcessDefinition : Entity
{
    public string CamundaDeploymentId { get; private set; } = string.Empty;
    public string CamundaDefinitionId { get; private set; } = string.Empty;
    public ProcessKey Key { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public int Version { get; private set; }
    public string ResourceName { get; private set; } = string.Empty;
    public DateTime DeployedAt { get; private set; }

    private ProcessDefinition() { }

    public static ProcessDefinition Create(
        string camundaDeploymentId,
        string camundaDefinitionId,
        string key,
        string name,
        int version,
        string resourceName)
    {
        return new ProcessDefinition
        {
            CamundaDeploymentId = camundaDeploymentId,
            CamundaDefinitionId = camundaDefinitionId,
            Key = new ProcessKey(key),
            Name = name,
            Version = version,
            ResourceName = resourceName,
            DeployedAt = DateTime.UtcNow
        };
    }
}
