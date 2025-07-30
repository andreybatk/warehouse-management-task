namespace WarehouseManagement.Domain.Exceptions;
public class ValidationException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }
}
