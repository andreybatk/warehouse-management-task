using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.DataAccess.Repositories;

public class ResourceRepository(ApplicationDbContext context) : IResourceRepository
{
    public async Task<List<Resource>> GetAllAsync(EState? state = null)
    {
        var query = context.Resources.AsQueryable();

        if (state is not null)
            query = query.Where(r => r.State == state);

        return await query.ToListAsync();
    }

    public async Task<Resource?> GetByIdAsync(Guid id)
    {
        return await context.Resources.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Guid> AddAsync(Resource resource)
    {
        context.Resources.Add(resource);
        await context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<Guid> UpdateAsync(Resource resource)
    {
        context.Resources.Update(resource);
        await context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<Guid?> DeleteAsync(Guid id)
    {
        var entity = await context.Resources.FirstOrDefaultAsync(r => r.Id == id);
        if (entity is null)
            return null;

        context.Resources.Remove(entity);
        await context.SaveChangesAsync();
        return id;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await context.Resources.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Resources.AnyAsync(r => r.Name == name);
    }

    public async Task<Resource?> GetByNameAsync(string name)
    {
        return await context.Resources.FirstOrDefaultAsync(r => r.Name == name);
    }
}
