using EPharmacy.Application.Prescriptions.Ponea.Commands.DTOs;
using FluentValidation;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.CreatePrescription;

public class CreatePrescriptionCommandValidator : AbstractValidator<CreatePrescriptionCommand>
{
    public CreatePrescriptionCommandValidator()
    {
        RuleFor(x => x.PatientPrescription.PharmacyIDs).NotNull();

        RuleFor(x => x.PatientPrescription.PharmacyIDs.Count)
            .NotEqual(0).WithMessage("PharmacyIDs must be at least 1");

        RuleFor(x => x.PatientPrescription.Patient).NotNull();

        RuleFor(x => x.PatientPrescription.PharmaceuticalItems)
            .NotEmpty().When(x => x.PatientPrescription.NonPharmaceuticalItems.Count == 0);

        RuleForEach(x => x.PatientPrescription.PharmaceuticalItems)
            .ChildRules(pharmItem =>
            {
                pharmItem.RuleFor(y => y.Drug).SetValidator(new CreateDrugValidator());
                pharmItem.RuleFor(y => y.Dosage).NotEmpty().NotNull();
                pharmItem.RuleFor(y => y.Duration).NotEmpty().NotNull();
                pharmItem.RuleFor(y => y.Frequency).NotEmpty().NotNull();
                pharmItem.RuleFor(y => y.FormulationID).NotEmpty().NotNull();
            });
    }

    public class CreateDrugValidator : AbstractValidator<CreateDrugDTO>
    {
        public CreateDrugValidator() => RuleFor(x => x.Name).NotNull();
    }
}