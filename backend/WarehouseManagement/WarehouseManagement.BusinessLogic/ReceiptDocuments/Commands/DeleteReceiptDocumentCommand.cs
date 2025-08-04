using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;

/// <summary>
/// Команда на удаление документа поступления
/// </summary>
/// <param name="Id">Идентификатор документа поступления</param>
public class DeleteReceiptDocumentCommand(Guid Id) : ICommand<Guid?>;