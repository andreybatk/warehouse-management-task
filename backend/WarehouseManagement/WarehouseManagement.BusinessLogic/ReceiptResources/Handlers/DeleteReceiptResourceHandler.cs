using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptResources.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptResources.Handlers;

public class DeleteReceiptResourceHandler(IReceiptResourceRepository resourceRepository)
    : ICommandHandler<DeleteReceiptResourceCommand, Guid>
{
    public async Task<Guid> Handle(DeleteReceiptResourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await resourceRepository.GetByIdAsync(request.Id);
        if (entity is null)
            throw new NotFoundException("Ресурс поступления не найден.");

        return await resourceRepository.DeleteAsync(entity);
    }
}