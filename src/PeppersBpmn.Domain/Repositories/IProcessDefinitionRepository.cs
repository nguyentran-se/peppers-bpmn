using PeppersBpmn.Domain.Entities;

namespace PeppersBpmn.Domain.Repositories;

public interface IProcessDefinitionRepository : IRepository<ProcessDefinition>
{
    Task<ProcessDefinition?> GetByKeyAsync(string key, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProcessDefinition>> GetByKeyAllVersionsAsync(string key, CancellationToken cancellationToken = default);
}
