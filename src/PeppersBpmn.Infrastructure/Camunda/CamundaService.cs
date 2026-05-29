using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Application.Services;

namespace PeppersBpmn.Infrastructure.Camunda;

public class CamundaService : ICamundaService
{
    private readonly CamundaClient _client;

    public CamundaService(CamundaClient client)
    {
        _client = client;
    }

    public async Task<DeploymentResultDto> DeployAsync(string bpmnXml, string resourceName, CancellationToken ct)
    {
        var response = await _client.DeployAsync(bpmnXml, resourceName, ct);

        var deployedDefs = (response.DeployedProcessDefinitions?.Values ?? Enumerable.Empty<Models.CamundaProcessDefinitionResponse>())
            .Select(d => new ProcessDefinitionDto(
                Guid.Empty,
                response.Id,
                d.Id,
                d.Key,
                d.Name ?? d.Key,
                d.Version,
                d.Resource ?? resourceName,
                response.DeploymentTime));

        return new DeploymentResultDto(response.Id, response.Name, response.DeploymentTime, deployedDefs);
    }

    public async Task<ProcessInstanceDto> StartProcessAsync(string processKey, Dictionary<string, object>? variables, CancellationToken ct)
    {
        var response = await _client.StartProcessAsync(processKey, variables, ct);

        return new ProcessInstanceDto(
            Guid.Empty,
            response.Id,
            processKey,
            response.BusinessKey ?? string.Empty,
            response.Suspended ? "Suspended" : "Active",
            DateTime.UtcNow,
            null);
    }

    public async Task CompleteTaskAsync(string taskId, Dictionary<string, object>? variables, CancellationToken ct)
    {
        await _client.CompleteTaskAsync(taskId, variables, ct);
    }

    public async Task<IEnumerable<TaskDto>> GetTasksAsync(string? processInstanceId, CancellationToken ct)
    {
        var tasks = await _client.GetTasksAsync(processInstanceId, ct);
        return tasks.Select(t => new TaskDto(
            t.Id,
            t.Name ?? string.Empty,
            t.Assignee,
            t.ProcessInstanceId,
            t.ProcessDefinitionId,
            t.Created,
            t.Due,
            t.TaskDefinitionKey));
    }

    public async Task<IEnumerable<ProcessInstanceDto>> GetProcessInstancesAsync(string? processKey, CancellationToken ct)
    {
        var instances = await _client.GetProcessInstancesAsync(processKey, ct);
        return instances.Select(i => new ProcessInstanceDto(
            Guid.Empty,
            i.Id,
            string.Empty,
            i.BusinessKey ?? string.Empty,
            i.Suspended ? "Suspended" : "Active",
            DateTime.UtcNow,
            null));
    }

    public async Task<IEnumerable<ProcessDefinitionDto>> GetDeployedDefinitionsAsync(CancellationToken ct)
    {
        var defs = await _client.GetProcessDefinitionsAsync(ct);
        return defs.Select(d => new ProcessDefinitionDto(
            Guid.Empty,
            d.DeploymentId,
            d.Id,
            d.Key,
            d.Name ?? d.Key,
            d.Version,
            d.Resource ?? string.Empty,
            DateTime.UtcNow));
    }
}
