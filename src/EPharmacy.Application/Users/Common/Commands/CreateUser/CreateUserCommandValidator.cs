using FluentValidation;

namespace EPharmacy.Application.Users.Common.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User.FullName)
            .NotEmpty().WithMessage("FullName cannot be empty")
            .NotNull().WithMessage("FullName cannot be null");

        RuleFor(x => x.User.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .NotNull().WithMessage("Email cannot be null")
            .EmailAddress().WithMessage("A valid email is required");

        RuleFor(x => x.User.Gender)
            .NotEmpty().WithMessage("Gender cannot be empty")
            .NotNull().WithMessage("Gender cannot be null");

        RuleFor(x => x.User.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber cannot be empty")
            .NotNull().WithMessage("PhoneNumber cannot be null");
    }
}