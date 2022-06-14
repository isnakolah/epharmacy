using FluentValidation;

namespace EPharmacy.Application.Patients.Queries.GetConciergePatient;

public class GetPatientByConciergeIDQueryValidator : AbstractValidator<GetPatientByConciergeIDQuery>
{
    public GetPatientByConciergeIDQueryValidator()
    {
        RuleFor(x => x.ConciergeID)
            .NotNull().WithMessage("ConciergeID is required")
            .NotEmpty().WithMessage("ConciergeID is required");
    }
}