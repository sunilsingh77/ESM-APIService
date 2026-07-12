using EmployeeSkills.Application.Departments.Interfaces;
using EmployeeSkills.Application.Departments.Services;
using EmployeeSkills.Application.Employees.Interfaces;
using EmployeeSkills.Application.Employees.Services;
using EmployeeSkills.Application.EmployeeSkills.Interfaces;
using EmployeeSkills.Application.EmployeeSkills.Services;
using EmployeeSkills.Application.Home.Interfaces;
using EmployeeSkills.Application.Home.Services;
using EmployeeSkills.Application.Skills.Interfaces;
using EmployeeSkills.Application.Skills.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSkills.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IEmployeeSkillService, EmployeeSkillService>();
        services.AddScoped<IHomeService, HomeService>();

        // Register ALL validators in the Application assembly
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

        return services;
    }
}