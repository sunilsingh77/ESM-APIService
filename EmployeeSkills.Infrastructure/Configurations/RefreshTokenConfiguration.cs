using EmployeeSkills.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeSkills.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        // Primary Key
        builder.HasKey(rt => rt.Id);

        // Properties
        builder.Property(rt => rt.Token)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(rt => rt.UserId)
               .IsRequired();

        builder.Property(rt => rt.Created)
               .IsRequired();

        builder.Property(rt => rt.Expires)
               .IsRequired();

        builder.Property(rt => rt.Revoked);

        // Indexes
        builder.HasIndex(rt => rt.Token)
               .IsUnique();

        builder.HasIndex(rt => rt.UserId);

        // Relationships
        builder.HasOne(rt => rt.User)
               .WithMany(u => u.RefreshTokens)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}