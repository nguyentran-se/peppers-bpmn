using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeppersBpmn.Application.Commands.CompleteTask;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Application.Queries.GetTasks;

namespace PeppersBpmn.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasks([FromQuery] string? processInstanceId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetTasksQuery(processInstanceId), ct);
        return Ok(result);
    }

    [HttpPost("{taskId}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CompleteTask(string taskId, [FromBody] CompleteTaskRequest request, CancellationToken ct)
    {
        await _mediator.Send(new CompleteTaskCommand(taskId, request.Variables), ct);
        return NoContent();
    }
}

public record CompleteTaskRequest(Dictionary<string, object>? Variables);
