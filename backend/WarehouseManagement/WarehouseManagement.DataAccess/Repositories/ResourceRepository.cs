using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.DataAccess.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _context;

    public ResourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Resource>> GetAllAsync(State? state = null)
    {
        var query = _context.Resources.AsQueryable();

        if (state is not null)
            query = query.Where(r => r.State == state);

        return await query.ToListAsync();
    }

    public async Task<Resource?> GetByIdAsync(Guid id)
    {
        return await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Resource?> GetByNameAsync(string name)
    {
        return await _context.Resources.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<Guid> AddAsync(Resource resource)
    {
        resource.Id = Guid.NewGuid();
        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<Guid> UpdateAsync(Resource resource)
    {
        _context.Resources.Update(resource);
        await _context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
        if (entity is null)
            return false;

        _context.Resources.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Resources.AnyAsync(r => r.Name == name);
    }
}
