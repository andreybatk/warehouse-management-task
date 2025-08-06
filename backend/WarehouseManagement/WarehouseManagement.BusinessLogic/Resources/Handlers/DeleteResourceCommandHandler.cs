using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class DeleteResourceCommandHandler(IResourceRepository resourceRepository, IReceiptResourceRepository receiptResourceRepository, ILogger<DeleteResourceCommandHandler> logger)
    : ICommandHandler<DeleteResourceCommand, Guid?>
{
    public async Task<Guid?> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        if (!await resourceRepository.ExistsByIdAsync(request.Id))
        {
            logger.LogError("Ресурс '{id}' не найден.", request.Id);
            throw new NotFoundException("Ресурс не найден.");
        }

        if (await receiptResourceRepository.IsResourceUsedAsync(request.Id))
        {
            logger.LogError("Ресурс '{id}' не может быть удален, так как используется в ресурсе поступления.", request.Id);
            throw new NotFoundException("Ресурс не может быть удален, так как используется в ресурсе поступления.");
        }

        var deletedId = await resourceRepository.DeleteAsync(request.Id);

        return deletedId;
    }
}