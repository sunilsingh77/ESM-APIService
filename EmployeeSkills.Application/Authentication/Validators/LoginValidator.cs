using EmployeeSkills.Application.Authentication.DTOs;
using FluentValidation;

namespace EmployeeSkills.Application.Authentication.Validators;

public class LoginValidator
    : AbstractValidator<UserLoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}