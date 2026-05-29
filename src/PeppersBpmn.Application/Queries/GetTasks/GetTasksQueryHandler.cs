using MediatR;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Application.Services;

namespace PeppersBpmn.Application.Queries.GetTasks;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
{
    private readonly ICamundaService _camundaService;

    public GetTasksQueryHandler(ICamundaService camundaService)
    {
        _camundaService = camundaService;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _camundaService.GetTasksAsync(request.ProcessInstanceId, cancellationToken);
    }
}
