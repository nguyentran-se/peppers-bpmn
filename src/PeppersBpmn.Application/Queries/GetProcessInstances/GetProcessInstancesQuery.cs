using MediatR;
using PeppersBpmn.Application.DTOs;

namespace PeppersBpmn.Application.Queries.GetProcessInstances;

public record GetProcessInstancesQuery(string? ProcessKey = null, int Page = 1, int PageSize = 20) : IRequest<PagedResult<ProcessInstanceDto>>;
