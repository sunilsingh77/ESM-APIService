using EmployeeSkills.Application.Employees.DTOs;
using FluentValidation;

namespace EmployeeSkills.Application.Employees.Validators;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.Position)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);

        RuleFor(x => x.HireDate)
            .LessThanOrEqualTo(DateTime.Today);
    }
}