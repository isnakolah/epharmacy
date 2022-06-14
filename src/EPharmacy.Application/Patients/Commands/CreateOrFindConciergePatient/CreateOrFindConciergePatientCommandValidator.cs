using FluentValidation;

namespace EPharmacy.Application.Patients.Commands.CreateOrFindConciergePatient;

public class CreateOrFindConciergePatientCommandValidator : AbstractValidator<CreateOrFindConciergePatientCommand>
{
    public CreateOrFindConciergePatientCommandValidator()
    {
        RuleFor(x => x.PatientPrescription.Name)
            .NotEmpty().WithMessage("Patient email required, cannot be empty")
            .NotNull().WithMessage("Patient email required, cannot be null");

        RuleFor(x => x.PatientPrescription.Email)
            .NotEmpty().WithMessage("Patient email required, cannot be empty")
            .NotNull().WithMessage("Patient email required, cannot be null");

        RuleFor(x => x.PatientPrescription.Phone)
            .NotEmpty().WithMessage("Patient phone number required, cannot be empty")
            .NotEmpty().WithMessage("Patient phone number required, cannot be null");
    }
}