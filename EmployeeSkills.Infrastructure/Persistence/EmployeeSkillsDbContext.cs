using EmployeeSkills.Domain.Entities;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkills.Infrastructure.Persistence;

public class EmployeeSkillsDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public EmployeeSkillsDbContext(DbContextOptions<EmployeeSkillsDbContext> options) : base(options)
    {
    }
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<EmployeeSkill> EmployeeSkills => Set<EmployeeSkill>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(EmployeeSkillsDbContext).Assembly);
    }
}
    