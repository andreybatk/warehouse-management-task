using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptResources.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptResources.Handlers;

public class UpdateReceiptResourceHandler(
    IReceiptResourceRepository receiptResourceRepository,
    IReceiptDocumentRepository documentRepository,
    IResourceRepository resourceRepository,
    IUnitRepository unitRepository)
    : ICommandHandler<UpdateReceiptResourceCommand, Guid>
{
    public async Task<Guid> Handle(UpdateReceiptResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await receiptResourceRepository.GetByIdAsync(request.Id);
        if (entity is null)
            throw new NotFoundException("Ресурс поступления не найден.");

        if (!await documentRepository.ExistsByIdAsync(request.ReceiptDocumentId))
            throw new NotFoundException($"Документ поступления не найден.");

        if (!await resourceRepository.ExistsByIdAsync(request.ResourceId))
            throw new NotFoundException("Ресурс не найден.");

        if (!await unitRepository.ExistsByIdAsync(request.UnitId))
            throw new NotFoundException("Единица измерения не найдена.");

        entity.ResourceId = request.ResourceId;
        entity.UnitId = request.UnitId;
        entity.Quantity = request.Quantity;

        return await receiptResourceRepository.UpdateAsync(entity);
    }
}

