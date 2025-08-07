using FluentValidation;
using WarehouseManagement.BusinessLogic.Resources.Commands;

namespace WarehouseManagement.BusinessLogic.Resources.Validators;

// ReSharper disable once UnusedMember.Global Используется в контейнере зависимостей
public class UpdateResourceCommandValidator : AbstractValidator<UpdateResourceCommand>
{
    public UpdateResourceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Необходимо указать наименование ресурса.");
    }
}
