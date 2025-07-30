using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.Commands;

public sealed record CreateResourceCommand(
    string Name,
    State State
) : ICommand<Guid>;
