using EmployeeSkills.Domain.Entities;

namespace EmployeeSkills.Infrastructure.Persistence.Seed;

public static class SkillSeed
{
    public static async Task SeedAsync(EmployeeSkillsDbContext context)
    {
        if (context.Skills.Any())
            return;

        var skills = new List<Skill>
        {
            new Skill("ASP.NET Core","Web API Development","Backend"),
            new Skill("C#","Programming Language","Backend"),
            new Skill("SQL Server","Database","Database"),
            new Skill("Angular","Frontend","Frontend"),
            new Skill("React","Frontend","Frontend"),
            new Skill("Azure","Cloud","Cloud"),
            new Skill("Docker","Container","DevOps"),
            new Skill("Kubernetes","Container Orchestration","DevOps"),
            new Skill("Git","Version Control","Tools"),
            new Skill("Power BI","Reporting","Analytics")
        };

        await context.Skills.AddRangeAsync(skills);
        await context.SaveChangesAsync();
    }
}