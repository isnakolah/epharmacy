using FluentValidation;

namespace EPharmacy.Application.Formulations.Ponea.Commands.CreateFormulation;

public class CreateFormulationCommandValidator : AbstractValidator<CreateFormulationCommand>
{
    public CreateFormulationCommandValidator()
    {
        RuleFor(x => x.Formulation.Name).NotNull().NotEmpty();
    }
}