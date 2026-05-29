using MediatR;
using PeppersBpmn.Application.DTOs;

namespace PeppersBpmn.Application.Commands.StartProcess;

public record StartProcessCommand(string ProcessKey, string? BusinessKey, Dictionary<string, object>? Variables) : IRequest<ProcessInstanceDto>;
