using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PeppersBpmn.Infrastructure.Camunda.Models;

namespace PeppersBpmn.Infrastructure.Camunda;

public class CamundaClient
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public CamundaClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<CamundaDeploymentResponse> DeployAsync(string bpmnXml, string resourceName, CancellationToken ct)
    {
        var filename = resourceName.EndsWith(".bpmn", StringComparison.OrdinalIgnoreCase)
                    || resourceName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)
            ? resourceName
            : resourceName + ".bpmn";

        using var content = new MultipartFormDataContent();
        var xmlBytes = Encoding.UTF8.GetBytes(bpmnXml);
        var fileContent = new ByteArrayContent(xmlBytes);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(fileContent, "data", filename);
        content.Add(new StringContent(filename), "deployment-name");

        var response = await _http.PostAsync("deployment/create", content, ct);
        await EnsureSuccessAsync(response, ct);

        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<CamundaDeploymentResponse>(json, JsonOptions)!;
    }

    public async Task<CamundaProcessInstanceResponse> StartProcessAsync(string processKey, object? variables, CancellationToken ct)
    {
        var body = variables is null ? "{}" : JsonSerializer.Serialize(new { variables = WrapVariables(variables as Dictionary<string, object>) });
        var content = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync($"process-definition/key/{processKey}/start", content, ct);
        await EnsureSuccessAsync(response, ct);

        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<CamundaProcessInstanceResponse>(json, JsonOptions)!;
    }

    public async Task CompleteTaskAsync(string taskId, Dictionary<string, object>? variables, CancellationToken ct)
    {
        var payload = new { variables = WrapVariables(variables) };
        var body = JsonSerializer.Serialize(payload);
        var content = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync($"task/{taskId}/complete", content, ct);
        await EnsureSuccessAsync(response, ct);
    }

    public async Task<IEnumerable<CamundaTaskResponse>> GetTasksAsync(string? processInstanceId, CancellationToken ct)
    {
        var url = "task";
        if (!string.IsNullOrWhiteSpace(processInstanceId))
            url += $"?processInstanceId={Uri.EscapeDataString(processInstanceId)}";

        var response = await _http.GetAsync(url, ct);
        await EnsureSuccessAsync(response, ct);

        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<IEnumerable<CamundaTaskResponse>>(json, JsonOptions) ?? [];
    }

    public async Task<IEnumerable<CamundaProcessInstanceResponse>> GetProcessInstancesAsync(string? processKey, CancellationToken ct)
    {
        var url = "process-instance";
        if (!string.IsNullOrWhiteSpace(processKey))
            url += $"?processDefinitionKey={Uri.EscapeDataString(processKey)}";

        var response = await _http.GetAsync(url, ct);
        await EnsureSuccessAsync(response, ct);

        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<IEnumerable<CamundaProcessInstanceResponse>>(json, JsonOptions) ?? [];
    }

    public async Task<IEnumerable<CamundaProcessDefinitionResponse>> GetProcessDefinitionsAsync(CancellationToken ct)
    {
        var response = await _http.GetAsync("process-definition", ct);
        await EnsureSuccessAsync(response, ct);

        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<IEnumerable<CamundaProcessDefinitionResponse>>(json, JsonOptions) ?? [];
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken ct)
    {
        if (response.IsSuccessStatusCode) return;
        var body = await response.Content.ReadAsStringAsync(ct);
        throw new HttpRequestException(
            $"Camunda {(int)response.StatusCode}: {body}",
            null,
            response.StatusCode);
    }

    private static Dictionary<string, object>? WrapVariables(Dictionary<string, object>? variables)
    {
        if (variables is null) return null;
        return variables.ToDictionary(
            kv => kv.Key,
            kv => (object)new { value = kv.Value, type = InferType(kv.Value) });
    }

    private static string InferType(object value) => value switch
    {
        string => "String",
        int or long => "Integer",
        double or float or decimal => "Double",
        bool => "Boolean",
        _ => "String"
    };
}
