using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeppersBpmn.Application.Commands.StartProcess;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Application.Queries.GetProcessInstances;

namespace PeppersBpmn.Api.Controllers;

[ApiController]
[Route("api/process-instances")]
public class ProcessInstancesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcessInstancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("start")]
    [ProducesResponseType(typeof(ProcessInstanceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Start([FromBody] StartProcessRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new StartProcessCommand(request.ProcessKey, request.BusinessKey, request.Variables), ct);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProcessInstanceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] string? processKey, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetProcessInstancesQuery(processKey, page, pageSize), ct);
        return Ok(result);
    }
}

public record StartProcessRequest(string ProcessKey, string? BusinessKey, Dictionary<string, object>? Variables);
