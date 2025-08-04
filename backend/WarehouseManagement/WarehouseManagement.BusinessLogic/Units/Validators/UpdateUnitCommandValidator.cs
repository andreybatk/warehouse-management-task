using FluentValidation;
using WarehouseManagement.BusinessLogic.Units.Commands;

namespace WarehouseManagement.BusinessLogic.Units.Validators;
public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Необходимо указать наименование единицы измерения.");
    }
}