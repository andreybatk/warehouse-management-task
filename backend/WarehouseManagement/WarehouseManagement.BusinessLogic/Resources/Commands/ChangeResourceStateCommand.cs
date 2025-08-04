using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.Commands;

/// <summary>
/// Команда на изменение состояние ресурса
/// </summary>
/// <param name="Id">Идентификатор ресурса</param>
/// <param name="NewState">Новое состояние</param>
public record ChangeResourceStateCommand(Guid Id, EState NewState) : ICommand<Guid>;
