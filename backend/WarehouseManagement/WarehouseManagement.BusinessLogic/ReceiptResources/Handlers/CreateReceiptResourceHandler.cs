using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptResources.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptResources.Handlers;

public class CreateReceiptResourceHandler(
    IReceiptResourceRepository receiptResourceRepository,
    IReceiptDocumentRepository documentRepository,
    IResourceRepository resourceRepository,
    IUnitRepository unitRepository)
    : ICommandHandler<AddReceiptResourceCommand, Guid>
{
    public async Task<Guid> Handle(AddReceiptResourceCommand request, CancellationToken cancellationToken)
    {
        if (!await documentRepository.ExistsByIdAsync(request.ReceiptDocumentId))
            throw new NotFoundException("Документ поступления не найден.");

        if (!await resourceRepository.ExistsByIdAsync(request.ResourceId))
            throw new NotFoundException("Ресурс не найден.");

        if (!await unitRepository.ExistsByIdAsync(request.UnitId))
            throw new NotFoundException("Единица измерения не найдена.");

        var entity = new ReceiptResource
        {
            Id = Guid.NewGuid(),
            ReceiptDocumentId = request.ReceiptDocumentId,
            ResourceId = request.ResourceId,
            UnitId = request.UnitId,
            Quantity = request.Quantity
        };

        return await receiptResourceRepository.AddAsync(entity);
    }
}

