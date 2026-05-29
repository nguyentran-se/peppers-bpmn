using MediatR;
using PeppersBpmn.Application.DTOs;
using PeppersBpmn.Application.Services;
using PeppersBpmn.Domain.Entities;
using PeppersBpmn.Domain.Repositories;

namespace PeppersBpmn.Application.Commands.DeployProcess;

public class DeployProcessCommandHandler : IRequestHandler<DeployProcessCommand, DeploymentResultDto>
{
    private readonly ICamundaService _camundaService;
    private readonly IProcessDefinitionRepository _definitionRepository;

    public DeployProcessCommandHandler(ICamundaService camundaService, IProcessDefinitionRepository definitionRepository)
    {
        _camundaService = camundaService;
        _definitionRepository = definitionRepository;
    }

    public async Task<DeploymentResultDto> Handle(DeployProcessCommand request, CancellationToken cancellationToken)
    {
        var result = await _camundaService.DeployAsync(request.BpmnXml, request.ResourceName, cancellationToken);

        foreach (var def in result.DeployedDefinitions)
        {
            var entity = ProcessDefinition.Create(
                result.DeploymentId,
                def.CamundaDefinitionId,
                def.Key,
                def.Name,
                def.Version,
                def.ResourceName);

            await _definitionRepository.AddAsync(entity, cancellationToken);
        }

        return result;
    }
}
