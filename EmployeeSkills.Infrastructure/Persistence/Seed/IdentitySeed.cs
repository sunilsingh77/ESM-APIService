using EmployeeSkills.Domain.Entities;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeSkills.Infrastructure.Persistence.Seed;

public static class IdentitySeed
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var roles = new[]
        {
            "Admin",
            "Manager",
            "Employee"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var admin = await userManager.FindByEmailAsync("admin@company.com");

        if (admin == null)
        {
            admin = new ApplicationUser(
                "admin",
                "admin@company.com");

            await userManager.CreateAsync(admin, "Admin@123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}