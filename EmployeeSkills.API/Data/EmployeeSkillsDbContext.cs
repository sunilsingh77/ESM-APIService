using EmployeeSkills.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.API.Data
{
    public class EmployeeSkillsDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public EmployeeSkillsDbContext(DbContextOptions<EmployeeSkillsDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<EmployeeSkill> EmployeeSkills => Set<EmployeeSkill>();
        public DbSet<Skill> Skills => Set<Skill>();

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
