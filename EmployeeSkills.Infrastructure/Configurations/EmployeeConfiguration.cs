using EmployeeSkills.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSkills.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        // Primary Key
        builder.HasKey(e => e.Id);

        // Properties
        builder.Property(e => e.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.LastName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(e => e.PhoneNumber)
               .HasMaxLength(20);

        builder.Property(e => e.Position)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.HireDate)
               .IsRequired();

        builder.Property(e => e.CreatedDate)
               .IsRequired();

        builder.Property(e => e.UpdatedDate);

        // Indexes
        builder.HasIndex(e => e.Email)
               .IsUnique();

        builder.HasIndex(e => e.DepartmentId);

        // Relationships
        builder.HasOne(e => e.Department)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.EmployeeSkills)
               .WithOne(es => es.Employee)
               .HasForeignKey(es => es.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}