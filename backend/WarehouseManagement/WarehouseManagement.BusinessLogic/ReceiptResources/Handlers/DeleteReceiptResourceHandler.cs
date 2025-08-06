using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptResources.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptResources.Handlers;

public class DeleteReceiptResourceHandler(IReceiptResourceRepository receiptResourceRepository)
    : ICommandHandler<DeleteReceiptResourceCommand, Guid>
{
    public async Task<Guid> Handle(DeleteReceiptResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await receiptResourceRepository.GetByIdAsync(request.Id);
        if (entity is null)
            throw new NotFoundException("Ресурс поступления не найден.");

        return await receiptResourceRepository.DeleteAsync(entity);
    }
}