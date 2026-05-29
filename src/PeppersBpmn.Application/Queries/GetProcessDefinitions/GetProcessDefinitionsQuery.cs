using MediatR;
using PeppersBpmn.Application.DTOs;

namespace PeppersBpmn.Application.Queries.GetProcessDefinitions;

public record GetProcessDefinitionsQuery(int Page = 1, int PageSize = 20) : IRequest<PagedResult<ProcessDefinitionDto>>;
