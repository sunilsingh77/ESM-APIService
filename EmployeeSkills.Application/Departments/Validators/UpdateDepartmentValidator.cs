using EmployeeSkills.Application.Departments.DTOs;
using FluentValidation;

namespace EmployeeSkills.Application.Departments.Validators;

public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDto>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}