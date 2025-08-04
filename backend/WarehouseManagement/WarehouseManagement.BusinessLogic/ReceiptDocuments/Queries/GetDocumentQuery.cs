using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Queries;
/// <summary>
/// Запрос на получение документа поступления
/// </summary>
/// <param name="Id">Идентификатор документа поступления</param>
public sealed record GetDocumentQuery(Guid Id) : IQuery<ReceiptDocumentResponse>;
