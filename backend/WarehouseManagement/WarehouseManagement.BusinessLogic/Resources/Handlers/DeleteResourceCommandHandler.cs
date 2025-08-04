using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class DeleteResourceCommandHandler(IResourceRepository resourceRepository, ILogger<DeleteResourceCommandHandler> logger)
    : ICommandHandler<DeleteResourceCommand, Guid?>
{
    public async Task<Guid?> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        if (await resourceRepository.ExistsByIdAsync(request.Id))
        {
            logger.LogError("Ресурс '{id}' не найден.", request.Id);
            throw new NotFoundException("Ресурс не найден.");
        }

        //TODO: проверка на использовании в документах поступления

        var deletedId = await resourceRepository.DeleteAsync(request.Id);

        return deletedId;
    }
}