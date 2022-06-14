using FluentValidation;

namespace EPharmacy.Application.Prescriptions.Ponea.Commands.CancelPrescription;

public class CancelPrescriptionCommandValidator : AbstractValidator<CancelPrescriptionCommand>
{
    public CancelPrescriptionCommandValidator()
    {
        RuleFor(x => x.ID).NotEmpty().NotEmpty();
    }
}
