using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class CreateResourceCommandHandler(IResourceRepository resourceRepository, ILogger<CreateResourceCommandHandler> logger)
    : ICommandHandler<CreateResourceCommand, Guid>
{
    public async Task<Guid> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        if (await resourceRepository.ExistsByNameAsync(request.Name))
        {
            logger.LogError("Ресурс таким наименованием '{name}' уже существует.", request.Name);
            throw new BadRequestException("Ресурс с таким наименованием уже существует.");
        }

        var resource = new Resource { Name = request.Name, State = request.State };

        return await resourceRepository.AddAsync(resource);
    }
}
