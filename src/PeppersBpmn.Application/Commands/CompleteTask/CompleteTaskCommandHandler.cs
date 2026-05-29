using MediatR;
using PeppersBpmn.Application.Services;

namespace PeppersBpmn.Application.Commands.CompleteTask;

public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand>
{
    private readonly ICamundaService _camundaService;

    public CompleteTaskCommandHandler(ICamundaService camundaService)
    {
        _camundaService = camundaService;
    }

    public async Task Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        await _camundaService.CompleteTaskAsync(request.TaskId, request.Variables, cancellationToken);
    }
}
