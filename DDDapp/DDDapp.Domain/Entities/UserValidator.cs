using FluentValidation;

namespace DDDapp.Domain.Entities;
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().Length(0, 60).WithMessage("Please specify a name");
        RuleFor(x => x.LastName).NotNull().NotEmpty().Length(0, 60).WithMessage("Please specify a lastname");
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Please specify an email");
    }
}