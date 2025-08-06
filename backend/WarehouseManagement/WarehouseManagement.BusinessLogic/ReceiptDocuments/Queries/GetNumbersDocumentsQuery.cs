using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Queries;
/// <summary>
/// Запрос на получение номеров документа поступления
/// </summary>
public sealed record GetNumbersDocumentsQuery : IQuery<List<long>>;
