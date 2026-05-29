using MediatR;
using PeppersBpmn.Application.DTOs;

namespace PeppersBpmn.Application.Commands.DeployProcess;

public record DeployProcessCommand(string BpmnXml, string ResourceName) : IRequest<DeploymentResultDto>;
