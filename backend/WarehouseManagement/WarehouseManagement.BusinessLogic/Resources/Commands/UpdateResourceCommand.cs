using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.Resources.Commands;
public sealed record UpdateResourceCommand(
    Guid Id,
    string Name
) : ICommand<Guid>;
