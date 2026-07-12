using EmployeeSkills.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSkills.Infrastructure.DependencyInjection;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmployeeSkillsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                            sqlOptions =>
                                                                            {
                                                                                sqlOptions.MigrationsAssembly(
                                                                                typeof(EmployeeSkillsDbContext).Assembly.FullName);
                                                                            });
        });
        return services;
    }
}