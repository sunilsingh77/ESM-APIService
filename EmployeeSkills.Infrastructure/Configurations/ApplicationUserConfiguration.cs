using EmployeeSkills.Domain.Entities;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSkills.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Identity already maps this entity to AspNetUsers.
        // Don't call ToTable() unless you intentionally want a different table name.

        builder.Property(u => u.UserName)
               .HasMaxLength(256);

        builder.Property(u => u.NormalizedUserName)
               .HasMaxLength(256);

        builder.Property(u => u.Email)
               .HasMaxLength(256);

        builder.Property(u => u.NormalizedEmail)
               .HasMaxLength(256);

        builder.HasIndex(u => u.Email);

        builder.Property(u => u.FullName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(u => u.CreatedDate)
               .IsRequired();

        builder.HasMany(u => u.RefreshTokens)
               .WithOne(rt => rt.User)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}