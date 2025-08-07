using FluentValidation;
using WarehouseManagement.BusinessLogic.Units.Commands;

namespace WarehouseManagement.BusinessLogic.Units.Validators;

// ReSharper disable once UnusedMember.Global Используется в контейнере зависимостей
public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Необходимо указать наименование единицы измерения.");
    }
}