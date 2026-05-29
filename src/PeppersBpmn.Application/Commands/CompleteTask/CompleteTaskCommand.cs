using MediatR;

namespace PeppersBpmn.Application.Commands.CompleteTask;

public record CompleteTaskCommand(string TaskId, Dictionary<string, object>? Variables) : IRequest;
