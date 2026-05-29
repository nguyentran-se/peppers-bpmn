using PeppersBpmn.Domain.ValueObjects;

namespace PeppersBpmn.Domain.Entities;

public class ProcessInstance : Entity
{
    public string CamundaInstanceId { get; private set; } = string.Empty;
    public ProcessKey ProcessKey { get; private set; } = null!;
    public string BusinessKey { get; private set; } = string.Empty;
    public ProcessStatus Status { get; private set; } = ProcessStatus.Active;
    public DateTime StartedAt { get; private set; }
    public DateTime? EndedAt { get; private set; }

    private ProcessInstance() { }

    public static ProcessInstance Create(string camundaInstanceId, string processKey, string? businessKey = null)
    {
        return new ProcessInstance
        {
            CamundaInstanceId = camundaInstanceId,
            ProcessKey = new ProcessKey(processKey),
            BusinessKey = businessKey ?? string.Empty,
            Status = ProcessStatus.Active,
            StartedAt = DateTime.UtcNow
        };
    }

    public void UpdateStatus(string camundaStatus)
    {
        Status = ProcessStatus.From(camundaStatus);
        if (Status == ProcessStatus.Completed
            || Status == ProcessStatus.ExternallyTerminated
            || Status == ProcessStatus.InternallyTerminated)
        {
            EndedAt = DateTime.UtcNow;
        }
    }
}
