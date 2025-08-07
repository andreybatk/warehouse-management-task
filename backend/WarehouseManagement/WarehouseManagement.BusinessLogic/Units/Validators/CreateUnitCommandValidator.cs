using FluentValidation;
using WarehouseManagement.BusinessLogic.Units.Commands;

namespace WarehouseManagement.BusinessLogic.Units.Validators;

// ReSharper disable once UnusedMember.Global Используется в контейнере зависимостей
public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Необходимо указать наименование единицы измерения.");

        RuleFor(x => x.State)
            .IsInEnum().WithMessage("Необходимо указать состояние единицы измерения.");
    }
}