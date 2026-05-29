namespace PeppersBpmn.Application.DTOs;

public record DeploymentResultDto(
    string DeploymentId,
    string Name,
    string DeployedAt,
    IEnumerable<ProcessDefinitionDto> DeployedDefinitions);
