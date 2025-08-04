namespace WarehouseManagement.Domain.Exceptions;
public class ValidationException(IReadOnlyDictionary<string, string[]> errors) : Exception("Validation failed")
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}
