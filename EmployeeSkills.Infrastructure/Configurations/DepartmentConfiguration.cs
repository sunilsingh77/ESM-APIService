using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSkills.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        // Primary Key
        builder.HasKey(d => d.Id);

        // Properties
        builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(d => d.Description)
               .HasMaxLength(500);

        builder.Property(d => d.CreatedDate)
               .IsRequired();

        // Unique Index
        builder.HasIndex(d => d.Name)
               .IsUnique();

        // Relationships
        builder.HasMany(d => d.Employees)
               .WithOne(e => e.Department)
               .HasForeignKey(e => e.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}