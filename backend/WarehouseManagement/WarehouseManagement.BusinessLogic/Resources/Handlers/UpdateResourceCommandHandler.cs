using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class UpdateResourceCommandHandler : ICommandHandler<UpdateResourceCommand, Guid>
{
    private readonly IResourceRepository _resourceRepository;

    public UpdateResourceCommandHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<Guid> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _resourceRepository.GetByIdAsync(request.Id);
        if (resource is null)
            throw new NotFoundException(nameof(Resource), request.Id);

        resource.Name = request.Name;

        var updatedId = await _resourceRepository.UpdateAsync(resource);

        return updatedId;
    }
}
