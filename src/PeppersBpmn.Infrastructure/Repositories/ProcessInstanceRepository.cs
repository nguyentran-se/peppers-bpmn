using Microsoft.EntityFrameworkCore;
using PeppersBpmn.Domain.Entities;
using PeppersBpmn.Domain.Repositories;
using PeppersBpmn.Infrastructure.Persistence;

namespace PeppersBpmn.Infrastructure.Repositories;

public class ProcessInstanceRepository : IProcessInstanceRepository
{
    private readonly AppDbContext _db;

    public ProcessInstanceRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ProcessInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.ProcessInstances.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<ProcessInstance>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _db.ProcessInstances.OrderByDescending(x => x.StartedAt).ToListAsync(cancellationToken);

    public async Task AddAsync(ProcessInstance entity, CancellationToken cancellationToken = default)
    {
        await _db.ProcessInstances.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProcessInstance entity, CancellationToken cancellationToken = default)
    {
        _db.ProcessInstances.Update(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProcessInstance entity, CancellationToken cancellationToken = default)
    {
        _db.ProcessInstances.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProcessInstance?> GetByCamundaInstanceIdAsync(string camundaInstanceId, CancellationToken cancellationToken = default)
        => await _db.ProcessInstances.FirstOrDefaultAsync(x => x.CamundaInstanceId == camundaInstanceId, cancellationToken);

    public async Task<IEnumerable<ProcessInstance>> GetByProcessKeyAsync(string processKey, CancellationToken cancellationToken = default)
        => await _db.ProcessInstances
            .Where(x => EF.Property<string>(x, "ProcessKey") == processKey)
            .OrderByDescending(x => x.StartedAt)
            .ToListAsync(cancellationToken);
}
