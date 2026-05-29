namespace PeppersBpmn.Application.DTOs;

public record ProcessDefinitionDto(
    Guid Id,
    string CamundaDeploymentId,
    string CamundaDefinitionId,
    string Key,
    string Name,
    int Version,
    string ResourceName,
    string DeployedAt);
