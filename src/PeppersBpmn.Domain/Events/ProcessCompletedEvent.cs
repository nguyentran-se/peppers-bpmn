namespace PeppersBpmn.Domain.Events;

public record ProcessCompletedEvent(Guid ProcessInstanceId, string CamundaInstanceId, string ProcessKey, DateTime CompletedAt);
