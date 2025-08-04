using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class ChangeResourceStateHandler(IResourceRepository resourceRepository, ILogger<ChangeResourceStateHandler> logger)
    : ICommandHandler<ChangeResourceStateCommand, Guid>
{
    public async Task<Guid> Handle(ChangeResourceStateCommand request, CancellationToken cancellationToken)
    {
        var resource = await resourceRepository.GetByIdAsync(request.Id);

        if (resource is null)
        {
            logger.LogError("Ресурс '{id}' не найден.", request.Id);
            throw new NotFoundException("Ресурс не найден.");
        }

        resource.State = request.NewState;

        var updatedId = await resourceRepository.UpdateAsync(resource);

        return updatedId;
    }
}
