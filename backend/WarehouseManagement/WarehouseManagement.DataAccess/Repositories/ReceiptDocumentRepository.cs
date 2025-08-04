using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.DataAccess.Repositories;

public class ReceiptDocumentRepository(ApplicationDbContext context) : IReceiptDocumentRepository
{
    public async Task AddAsync(ReceiptDocument document)
    {
        await context.ReceiptDocuments.AddAsync(document);
        await context.SaveChangesAsync();
    }

    public async Task<ReceiptDocument?> GetByIdAsync(Guid id, bool include = false)
    {
        IQueryable<ReceiptDocument> query = context.ReceiptDocuments;

        if (include)
        {
            query = query
                .Include(d => d.ReceiptResources)
                .ThenInclude(dr => dr.Resource)
                .Include(d => d.ReceiptResources)
                .ThenInclude(dr => dr.Unit);
        }

        return await query.FirstOrDefaultAsync(d => d.Id == id);
    }


    public async Task<Guid> UpdateAsync(ReceiptDocument unit)
    {
        context.ReceiptDocuments.Update(unit);
        await context.SaveChangesAsync();
        return unit.Id;
    }

    public async Task<bool> ExistsByNumberAsync(long number)
    {
        return await context.ReceiptDocuments.AnyAsync(d => d.Number == number);
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await context.ReceiptDocuments.AnyAsync(d => d.Id == id);
    }

    public async Task<Guid?> DeleteAsync(Guid id)
    {
        var entity = await context.ReceiptDocuments.FirstOrDefaultAsync(u => u.Id == id);
        if (entity is null)
            return null;

        context.ReceiptDocuments.Remove(entity);
        await context.SaveChangesAsync();
        return id;
    }

    public async Task<List<ReceiptDocument>> GetDocumentsAsync(
        DateTime? dateFrom,
        DateTime? dateTo,
        List<long>? documentNumbers,
        List<Guid>? resourceIds,
        List<Guid>? unitIds)
    {
        var query = context.ReceiptDocuments.AsQueryable();

        if (dateFrom.HasValue)
            query = query.Where(d => d.CreatedAt >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(d => d.CreatedAt <= dateTo.Value);
        if (documentNumbers is { Count: > 0 })
            query = query.Where(d => documentNumbers.Contains(d.Number));

        query = query
            .Include(d => d.ReceiptResources
                .Where(r =>
                    (resourceIds == null || resourceIds.Count == 0 || resourceIds.Contains(r.ResourceId)) &&
                    (unitIds == null || unitIds.Count == 0 || unitIds.Contains(r.UnitId))))
            .ThenInclude(r => r.Resource)
            .Include(d => d.ReceiptResources
                .Where(r =>
                    (resourceIds == null || resourceIds.Count == 0 || resourceIds.Contains(r.ResourceId)) &&
                    (unitIds == null || unitIds.Count == 0 || unitIds.Contains(r.UnitId))))
            .ThenInclude(r => r.Unit);

        return await query
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();
    }
}