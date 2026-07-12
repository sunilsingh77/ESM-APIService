using EmployeeSkills.Domain.Entities;

namespace EmployeeSkills.Infrastructure.Persistence.Seed;

public static class EmployeeSkillSeed
{
    public static async Task SeedAsync(EmployeeSkillsDbContext context)
    {
        if (context.EmployeeSkills.Any())
            return;

        var sunil = context.Employees.First(x => x.Email == "sunil@company.com");
        var rahul = context.Employees.First(x => x.Email == "rahul@company.com");

        var aspnet = context.Skills.First(x => x.Name == "ASP.NET Core");
        var angular = context.Skills.First(x => x.Name == "Angular");
        var sql = context.Skills.First(x => x.Name == "SQL Server");

        var list = new List<EmployeeSkill>
        {
            new EmployeeSkill(
                sunil.Id,
                aspnet.Id,
                "Expert",
                8,
                true),

            new EmployeeSkill(
                sunil.Id,
                angular.Id,
                "Advanced",
                6,
                false),

            new EmployeeSkill(
                sunil.Id,
                sql.Id,
                "Expert",
                8,
                false),

            new EmployeeSkill(
                rahul.Id,
                angular.Id,
                "Intermediate",
                3,
                true),

            new EmployeeSkill(
                rahul.Id,
                aspnet.Id,
                "Intermediate",
                3,
                false)
        };

        await context.EmployeeSkills.AddRangeAsync(list);

        await context.SaveChangesAsync();
    }
}