using PeppersBpmn.Application.DTOs;

namespace PeppersBpmn.Application.Services;

public interface ICamundaService
{
    Task<DeploymentResultDto> DeployAsync(string bpmnXml, string resourceName, CancellationToken ct = default);
    Task<ProcessInstanceDto> StartProcessAsync(string processKey, Dictionary<string, object>? variables = null, CancellationToken ct = default);
    Task CompleteTaskAsync(string taskId, Dictionary<string, object>? variables = null, CancellationToken ct = default);
    Task<IEnumerable<TaskDto>> GetTasksAsync(string? processInstanceId = null, CancellationToken ct = default);
    Task<IEnumerable<ProcessInstanceDto>> GetProcessInstancesAsync(string? processKey = null, CancellationToken ct = default);
    Task<IEnumerable<ProcessDefinitionDto>> GetDeployedDefinitionsAsync(CancellationToken ct = default);
}
