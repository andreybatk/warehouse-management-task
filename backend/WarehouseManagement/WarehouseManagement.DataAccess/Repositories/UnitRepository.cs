using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.DataAccess.Repositories;

public class UnitRepository (ApplicationDbContext context) : IUnitRepository
{
    public async Task<List<Unit>> GetAllAsync(EState? state = null)
    {
        var query = context.Units.AsQueryable();

        if (state is not null)
            query = query.Where(u => u.State == state);

        return await query.ToListAsync();
    }

    public async Task<Unit?> GetByIdAsync(Guid id)
    {
        return await context.Units.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Guid> AddAsync(Unit unit)
    {
        unit.Id = Guid.NewGuid();
        context.Units.Add(unit);
        await context.SaveChangesAsync();
        return unit.Id;
    }

    public async Task<Guid> UpdateAsync(Unit unit)
    {
        context.Units.Update(unit);
        await context.SaveChangesAsync();
        return unit.Id;
    }

    public async Task<Guid?> DeleteAsync(Guid id)
    {
        var entity = await context.Units.FirstOrDefaultAsync(u => u.Id == id);
        if (entity is null)
            return null;

        context.Units.Remove(entity);
        await context.SaveChangesAsync();
        return id;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Units.AnyAsync(u => u.Name == name);
    }

    public async Task<Unit?> GetByNameAsync(string name)
    {
        return await context.Units.FirstOrDefaultAsync(u => u.Name == name);
    }
}
