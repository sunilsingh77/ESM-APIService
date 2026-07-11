using EmployeeSkills.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSkills.Infrastructure.Persistence.Configurations;

public class EmployeeSkillConfiguration : IEntityTypeConfiguration<EmployeeSkill>
{
    public void Configure(EntityTypeBuilder<EmployeeSkill> builder)
    {
        builder.ToTable("EmployeeSkills");

        // Primary Key
        builder.HasKey(es => es.Id);

        // Properties
        builder.Property(es => es.ProficiencyLevel)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(es => es.YearsOfExperience)
               .IsRequired();

        builder.Property(es => es.IsPrimary)
               .HasDefaultValue(false);

        builder.Property(es => es.AcquiredDate)
               .IsRequired();

        builder.Property(es => es.LastUpdatedDate);

        // Indexes
        builder.HasIndex(es => new
        {
            es.EmployeeId,
            es.SkillId
        })
        .IsUnique();

        builder.HasIndex(es => es.EmployeeId);

        builder.HasIndex(es => es.SkillId);

        // Relationships
        builder.HasOne(es => es.Employee)
               .WithMany(e => e.EmployeeSkills)
               .HasForeignKey(es => es.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(es => es.Skill)
               .WithMany(s => s.EmployeeSkills)
               .HasForeignKey(es => es.SkillId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}