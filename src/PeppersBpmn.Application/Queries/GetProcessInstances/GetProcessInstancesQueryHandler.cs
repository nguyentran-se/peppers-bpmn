using MediatR;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Domain.Repositories;

namespace PeppersBpmn.Application.Queries.GetProcessInstances;

public class GetProcessInstancesQueryHandler : IRequestHandler<GetProcessInstancesQuery, PagedResult<ProcessInstanceDto>>
{
    private readonly IProcessInstanceRepository _repository;

    public GetProcessInstancesQueryHandler(IProcessInstanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<ProcessInstanceDto>> Handle(GetProcessInstancesQuery request, CancellationToken cancellationToken)
    {
        var all = string.IsNullOrWhiteSpace(request.ProcessKey)
            ? (await _repository.GetAllAsync(cancellationToken)).ToList()
            : (await _repository.GetByProcessKeyAsync(request.ProcessKey, cancellationToken)).ToList();

        var items = all
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(i => new ProcessInstanceDto(
                i.Id,
                i.CamundaInstanceId,
                i.ProcessKey.Value,
                i.BusinessKey,
                i.Status.Value.ToString(),
                i.StartedAt,
                i.EndedAt));

        return new PagedResult<ProcessInstanceDto>
        {
            Items = items,
            TotalCount = all.Count,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
