using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.DataAccess.Repositories;

public class ReceiptResourceRepository(ApplicationDbContext context) : IReceiptResourceRepository
{
    public async Task<Guid> AddAsync(ReceiptResource resource)
    {
        context.ReceiptResources.Add(resource);
        await context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<Guid> DeleteAsync(ReceiptResource resource)
    {
        context.ReceiptResources.Remove(resource);
        await context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<ReceiptResource?> GetByIdAsync(Guid id)
    {
        return await context.ReceiptResources.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Guid> UpdateAsync(ReceiptResource resource)
    {
        context.ReceiptResources.Update(resource);
        await context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<bool> IsUnitUsedAsync(Guid unitId)
    {
        return await context.ReceiptResources.AnyAsync(r => r.UnitId == unitId);
    }

    public async Task<bool> IsResourceUsedAsync(Guid resourceId)
    {
        return await context.ReceiptResources.AnyAsync(r => r.ResourceId == resourceId);
    }
}
