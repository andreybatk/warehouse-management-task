using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Domain.Interfaces;

public interface IReceiptDocumentRepository
{
    /// <summary>
    /// Добавить новый документ поступления
    /// </summary>
    /// <param name="document">Документ поступления</param>
    /// <returns></returns>
    Task AddAsync(ReceiptDocument document);

    /// <summary>
    /// Обновить документ поступления
    /// </summary>
    Task<Guid> UpdateAsync(ReceiptDocument unit);

    /// <summary>
    /// Получить документ поступления по идентификатору
    /// </summary>
    Task<ReceiptDocument?> GetByIdAsync(Guid id, bool include = false);

    /// <summary>
    /// Удалить документ поступления
    /// </summary>
    Task<Guid?> DeleteAsync(Guid id);

    /// <summary>
    /// Проверить, существует ли документ поступления с таким номером.
    /// </summary>
    Task<bool> ExistsByNumberAsync(long number);

    /// <summary>
    /// Проверить, существует ли документ поступления с таким идентификатором.
    /// </summary>
    Task<bool> ExistsByIdAsync(Guid id);

    /// <summary>
    /// Получает документы с их ресурсами по фильтрам
    /// </summary>
    Task<List<ReceiptDocument>> GetDocumentsAsync(
        DateTime? dateFrom,
        DateTime? dateTo,
        List<long>? documentNumbers,
        List<Guid>? resourceIds,
        List<Guid>? unitIds);
}