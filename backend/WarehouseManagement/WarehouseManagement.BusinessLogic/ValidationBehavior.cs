using FluentValidation;
using MediatR;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic;
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .Select(f => new { f.PropertyName, f.ErrorMessage })
            .ToList();

        if (errors.Count != 0)
            throw new Domain.Exceptions.ValidationException(errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()));

        return await next(cancellationToken);
    }
}