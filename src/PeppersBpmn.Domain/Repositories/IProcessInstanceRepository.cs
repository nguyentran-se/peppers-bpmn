using PeppersBpmn.Domain.Entities;

namespace PeppersBpmn.Domain.Repositories;

public interface IProcessInstanceRepository : IRepository<ProcessInstance>
{
    Task<ProcessInstance?> GetByCamundaInstanceIdAsync(string camundaInstanceId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProcessInstance>> GetByProcessKeyAsync(string processKey, CancellationToken cancellationToken = default);
}
