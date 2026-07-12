using EmployeeSkills.Application.Authentication.DTOs;
using FluentValidation;

namespace EmployeeSkills.Application.Authentication.Validators;

public class RegisterValidator
    : AbstractValidator<UserRegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain a lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain a number.");

        //RuleFor(x => x.ConfirmPassword)
        //    .Equal(x => x.Password)
        //    .WithMessage("Passwords do not match.");
    }
}