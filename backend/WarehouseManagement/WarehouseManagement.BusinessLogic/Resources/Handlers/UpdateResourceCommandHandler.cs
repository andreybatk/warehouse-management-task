using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class UpdateResourceCommandHandler(IResourceRepository resourceRepository, ILogger<UpdateResourceCommandHandler> logger)
    : ICommandHandler<UpdateResourceCommand, Guid>
{
    public async Task<Guid> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await resourceRepository.GetByIdAsync(request.Id);

        if (resource is null)
        {
            logger.LogError("Ресурс '{id}' не найден.", request.Id);
            throw new NotFoundException("Ресурс не найден.");
        }

        resource.Name = request.Name;

        var updatedId = await resourceRepository.UpdateAsync(resource);

        return updatedId;
    }
}
