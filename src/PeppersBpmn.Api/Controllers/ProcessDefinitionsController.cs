using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeppersBpmn.Application.Commands.DeployProcess;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Application.Queries.GetProcessDefinitions;

namespace PeppersBpmn.Api.Controllers;

[ApiController]
[Route("api/process-definitions")]
public class ProcessDefinitionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcessDefinitionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("deploy")]
    [ProducesResponseType(typeof(DeploymentResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Deploy([FromBody] DeployProcessRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeployProcessCommand(request.BpmnXml, request.ResourceName), ct);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProcessDefinitionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetProcessDefinitionsQuery(page, pageSize), ct);
        return Ok(result);
    }
}

public record DeployProcessRequest(string BpmnXml, string ResourceName);
