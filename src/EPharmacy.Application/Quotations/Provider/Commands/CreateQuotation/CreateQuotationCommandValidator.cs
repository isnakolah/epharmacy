using FluentValidation;

namespace EPharmacy.Application.Quotations.Provider.Commands.CreateQuotation;

public class CreateQuotationCommandValidator : AbstractValidator<CreateQuotationCommand>
{
    public CreateQuotationCommandValidator()
    {
    }
}