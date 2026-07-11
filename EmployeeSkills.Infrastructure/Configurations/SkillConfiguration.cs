using EmployeeSkills.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSkills.Infrastructure.Persistence.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");

        // Primary Key
        builder.HasKey(s => s.Id);

        // Properties
        builder.Property(s => s.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(s => s.Description)
               .HasMaxLength(500);

        builder.Property(s => s.Category)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(s => s.CreatedDate)
               .IsRequired();

        // Indexes
        builder.HasIndex(s => s.Name)
               .IsUnique();

        builder.HasIndex(s => s.Category);

        // Relationships
        builder.HasMany(s => s.EmployeeSkills)
               .WithOne(es => es.Skill)
               .HasForeignKey(es => es.SkillId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}