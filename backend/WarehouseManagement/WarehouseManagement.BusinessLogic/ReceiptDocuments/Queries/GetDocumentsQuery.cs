using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Queries;

/// <summary>
/// Запрос на получение документов поступления
/// </summary>
/// <param name="DateFrom">Фильтрация по дате ОТ</param>
/// <param name="DateTo">Фильтрация по дате ДО</param>
/// <param name="DocumentNumbers">Фильтрация по номеру документов</param>
/// <param name="ResourceIds">Фильтрация по идентификатору ресурсов</param>
/// <param name="UnitIds">Фильтрация по идентификатору единиц измерения</param>
public sealed record GetDocumentsQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    List<long>? DocumentNumbers,
    List<Guid>? ResourceIds,
    List<Guid>? UnitIds
) : IQuery<List<ReceiptDocumentResponse>>;