using EmployeeSkills.Domain.Entities;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSkills.Infrastructure.Persistence.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<EmployeeSkillsDbContext>();
        
        await context.Database.MigrateAsync();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await IdentitySeed.SeedAsync(userManager, roleManager);
        await DepartmentSeed.SeedAsync(context);
        await SkillSeed.SeedAsync(context);
        await EmployeeSeed.SeedAsync(context);
        await EmployeeSkillSeed.SeedAsync(context);
    }
}