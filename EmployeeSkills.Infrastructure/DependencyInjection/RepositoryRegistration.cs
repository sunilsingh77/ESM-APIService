using EmployeeSkills.Domain.Repositories;
using EmployeeSkills.Infrastructure.Persistence;
using EmployeeSkills.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSkills.Infrastructure.DependencyInjection;

public static class RepositoryRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IEmployeeSkillRepository, EmployeeSkillRepository>();

        return services;
    }
}