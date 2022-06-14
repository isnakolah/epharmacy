using FluentValidation;

namespace EPharmacy.Application.Pharmacies.Queries.GetPharmacyByID;

public class GetPharamacyByIDQueryValidator : AbstractValidator<GetPharmacyByIDQuery>
{
    public GetPharamacyByIDQueryValidator()
    {
        RuleFor(x => x.ID)
            .NotEmpty().WithMessage("id is required, id cannot be null")
            .NotEmpty().WithMessage("id is required, id cannot be empty");
    }
}