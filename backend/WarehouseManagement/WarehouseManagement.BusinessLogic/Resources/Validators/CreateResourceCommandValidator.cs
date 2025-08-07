using FluentValidation;
using WarehouseManagement.BusinessLogic.Resources.Commands;

namespace WarehouseManagement.BusinessLogic.Resources.Validators;

// ReSharper disable once UnusedMember.Global Используется в контейнере зависимостей
public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
{
    public CreateResourceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Необходимо указать наименование ресурса.");

        RuleFor(x => x.State)
            .IsInEnum().WithMessage("Необходимо указать состояние ресурса.");
    }
}
