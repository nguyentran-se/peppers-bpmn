namespace PeppersBpmn.Application.DTOs;

public record TaskDto(
    string Id,
    string Name,
    string? Assignee,
    string ProcessInstanceId,
    string ProcessDefinitionId,
    DateTime? Created,
    DateTime? Due,
    string? TaskDefinitionKey);
