namespace PeppersBpmn.Domain.Events;

public record ProcessStartedEvent(Guid ProcessInstanceId, string CamundaInstanceId, string ProcessKey, DateTime StartedAt);
