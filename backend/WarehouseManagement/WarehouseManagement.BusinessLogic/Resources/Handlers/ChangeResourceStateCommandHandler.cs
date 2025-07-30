using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.DataAccess.Repositories;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class ChangeResourceStateHandler : ICommandHandler<ChangeResourceStateCommand, Guid>
{
    private readonly IResourceRepository _resourceRepository;

    public ChangeResourceStateHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<Guid> Handle(ChangeResourceStateCommand request, CancellationToken cancellationToken)
    {
        var resource = await _resourceRepository.GetByIdAsync(request.Id);
        if (resource is null)
            throw new NotFoundException(nameof(Resource), request.Id);

        resource.State = request.NewState;

        var updatedId = await _resourceRepository.UpdateAsync(resource);

        return updatedId;
    }
}
