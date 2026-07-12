using EmployeeSkills.Domain.Entities;
using EmployeeSkillsSummary.Domain.Entities;
namespace EmployeeSkills.Infrastructure.Persistence.Seed;

public static class DepartmentSeed
{
    public static async Task SeedAsync(EmployeeSkillsDbContext context)
    {
        if (context.Departments.Any())
            return;
        var departments = new List<Department>
        {
            new Department("Information Technology","IT Department"),
            new Department("Human Resources","HR Department"),
            new Department("Finance","Finance Department"),
            new Department("Sales","Sales Department"),
            new Department("Marketing","Marketing Department"),
            new Department("Administration","Administration Department")
        };
        await context.Departments.AddRangeAsync(departments);
        await context.SaveChangesAsync();
    }
}