using EmployeeSkills.Domain.Entities;

namespace EmployeeSkills.Infrastructure.Persistence.Seed;

public static class EmployeeSeed
{
    public static async Task SeedAsync(EmployeeSkillsDbContext context)
    {
        if (context.Employees.Any())
            return;

        var it = context.Departments.First(x => x.Name == "Information Technology");
        var hr = context.Departments.First(x => x.Name == "Human Resources");

        var employees = new List<Employee>
        {
            new Employee(
                "Sunil",
                "Singh",
                "sunil@company.com",
                "9876543210",
                "Senior Software Engineer",
                it.Id,
                DateTime.UtcNow.AddYears(-5)),

            new Employee(
                "Rahul",
                "Sharma",
                "rahul@company.com",
                "9999999999",
                "Software Engineer",
                it.Id,
                DateTime.UtcNow.AddYears(-3)),

            new Employee(
                "Priya",
                "Verma",
                "priya@company.com",
                "8888888888",
                "HR Manager",
                hr.Id,
                DateTime.UtcNow.AddYears(-7))
        };

        await context.Employees.AddRangeAsync(employees);

        await context.SaveChangesAsync();
    }
}