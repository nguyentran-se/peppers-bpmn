namespace PeppersBpmn.Application.DTOs;

public record DeploymentResultDto(
    string DeploymentId,
    string Name,
    DateTime DeployedAt,
    IEnumerable<ProcessDefinitionDto> DeployedDefinitions);
