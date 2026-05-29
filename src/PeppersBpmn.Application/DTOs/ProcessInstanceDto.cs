namespace PeppersBpmn.Application.DTOs;

public record ProcessInstanceDto(
    Guid Id,
    string CamundaInstanceId,
    string ProcessKey,
    string BusinessKey,
    string Status,
    DateTime StartedAt,
    DateTime? EndedAt);
