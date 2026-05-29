using MediatR;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Domain.Repositories;

namespace PeppersBpmn.Application.Queries.GetProcessDefinitions;

public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, PagedResult<ProcessDefinitionDto>>
{
    private readonly IProcessDefinitionRepository _repository;

    public GetProcessDefinitionsQueryHandler(IProcessDefinitionRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<ProcessDefinitionDto>> Handle(GetProcessDefinitionsQuery request, CancellationToken cancellationToken)
    {
        var all = (await _repository.GetAllAsync(cancellationToken)).ToList();

        var items = all
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(d => new ProcessDefinitionDto(
                d.Id,
                d.CamundaDeploymentId,
                d.CamundaDefinitionId,
                d.Key.Value,
                d.Name,
                d.Version,
                d.ResourceName,
                d.DeployedAt));

        return new PagedResult<ProcessDefinitionDto>
        {
            Items = items,
            TotalCount = all.Count,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
