using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.Commands;

public record ChangeResourceStateCommand(Guid Id, State NewState) : ICommand<Guid>;
