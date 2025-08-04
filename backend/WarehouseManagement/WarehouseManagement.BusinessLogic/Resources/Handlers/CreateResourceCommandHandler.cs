using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class CreateResourceCommandHandler(IResourceRepository resourceRepository)
    : ICommandHandler<CreateResourceCommand, Guid>
{
    public async Task<Guid> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = new Resource { Name = request.Name, State = request.State };

        return await resourceRepository.AddAsync(resource);
    }
}
