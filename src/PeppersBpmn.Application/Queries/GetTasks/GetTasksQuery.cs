using MediatR;
using PeppersBpmn.Application.DTOs;

namespace PeppersBpmn.Application.Queries.GetTasks;

public record GetTasksQuery(string? ProcessInstanceId = null) : IRequest<IEnumerable<TaskDto>>;
