using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmployeeSkills.API.Data
{
    public class EmployeeSkillsDbContextFactory : IDesignTimeDbContextFactory<EmployeeSkillsDbContext>
    {
        public EmployeeSkillsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EmployeeSkillsDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=(localdb)\\mssqllocaldb;Database=EmployeeSkillsDB;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new EmployeeSkillsDbContext(optionsBuilder.Options);
        }
    }
}
