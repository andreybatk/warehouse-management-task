using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

public interface IReceiptResourceRepository
{
    Task<ReceiptResource?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(ReceiptResource resource);
    Task<Guid> UpdateAsync(ReceiptResource resource);
    Task<Guid> DeleteAsync(ReceiptResource resource);
    Task<bool> IsUnitUsedAsync(Guid unitId);
    Task<bool> IsResourceUsedAsync(Guid resourceId);
}
