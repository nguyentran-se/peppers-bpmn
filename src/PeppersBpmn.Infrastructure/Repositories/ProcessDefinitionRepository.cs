using Microsoft.EntityFrameworkCore;
using PeppersBpmn.Domain.Entities;
using PeppersBpmn.Domain.Repositories;
using PeppersBpmn.Infrastructure.Persistence;

namespace PeppersBpmn.Infrastructure.Repositories;

public class ProcessDefinitionRepository : IProcessDefinitionRepository
{
    private readonly AppDbContext _db;

    public ProcessDefinitionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ProcessDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.ProcessDefinitions.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<ProcessDefinition>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _db.ProcessDefinitions.OrderByDescending(x => x.DeployedAt).ToListAsync(cancellationToken);

    public async Task AddAsync(ProcessDefinition entity, CancellationToken cancellationToken = default)
    {
        await _db.ProcessDefinitions.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProcessDefinition entity, CancellationToken cancellationToken = default)
    {
        _db.ProcessDefinitions.Update(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProcessDefinition entity, CancellationToken cancellationToken = default)
    {
        _db.ProcessDefinitions.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProcessDefinition?> GetByKeyAsync(string key, CancellationToken cancellationToken = default)
        => await _db.ProcessDefinitions
            .OrderByDescending(x => x.Version)
            .FirstOrDefaultAsync(x => EF.Property<string>(x, "Key") == key, cancellationToken);

    public async Task<IEnumerable<ProcessDefinition>> GetByKeyAllVersionsAsync(string key, CancellationToken cancellationToken = default)
        => await _db.ProcessDefinitions
            .Where(x => EF.Property<string>(x, "Key") == key)
            .OrderByDescending(x => x.Version)
            .ToListAsync(cancellationToken);
}
