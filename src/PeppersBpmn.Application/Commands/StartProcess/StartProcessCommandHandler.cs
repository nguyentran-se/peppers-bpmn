using MediatR;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Application.Services;
using PeppersBpmn.Domain.Entities;
using PeppersBpmn.Domain.Repositories;

namespace PeppersBpmn.Application.Commands.StartProcess;

public class StartProcessCommandHandler : IRequestHandler<StartProcessCommand, ProcessInstanceDto>
{
    private readonly ICamundaService _camundaService;
    private readonly IProcessInstanceRepository _instanceRepository;

    public StartProcessCommandHandler(ICamundaService camundaService, IProcessInstanceRepository instanceRepository)
    {
        _camundaService = camundaService;
        _instanceRepository = instanceRepository;
    }

    public async Task<ProcessInstanceDto> Handle(StartProcessCommand request, CancellationToken cancellationToken)
    {
        var result = await _camundaService.StartProcessAsync(request.ProcessKey, request.Variables, cancellationToken);

        var entity = ProcessInstance.Create(result.CamundaInstanceId, request.ProcessKey, request.BusinessKey);
        await _instanceRepository.AddAsync(entity, cancellationToken);

        return result;
    }
}
