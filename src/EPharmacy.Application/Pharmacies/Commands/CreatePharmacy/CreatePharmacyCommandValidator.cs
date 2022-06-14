using FluentValidation;

namespace EPharmacy.Application.Pharmacies.Commands.CreatePharmacy;

public class CreatePharmacyCommandValidator : AbstractValidator<CreatePharmacyCommand>
{
    public CreatePharmacyCommandValidator()
    {
        RuleFor(x => x.Pharmacy.Name)
            .NotNull().WithMessage("Pharmacy name cannot be null")
            .NotEmpty().WithMessage("Pharmacy name required");

        RuleFor(x => x.Pharmacy.Location)
            .NotNull().WithMessage("Pharmacy location cannot be null")
            .NotEmpty().WithMessage("Pharmacy location required");
    }
}
