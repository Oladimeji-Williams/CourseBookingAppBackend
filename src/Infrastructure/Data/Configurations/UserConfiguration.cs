using CourseBookingAppBackend.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseBookingAppBackend.src.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Users");

        entityTypeBuilder.HasKey(u => u.Id);
        entityTypeBuilder.HasIndex(u => u.Email).IsUnique();

        entityTypeBuilder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        entityTypeBuilder.Property(u => u.PasswordHash)
            .IsRequired();

        entityTypeBuilder.Property(u => u.FirstName)
            .HasMaxLength(100);

        entityTypeBuilder.Property(u => u.LastName)
            .HasMaxLength(100);

        entityTypeBuilder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        entityTypeBuilder.Property(u => u.PhysicalAddress)
            .HasMaxLength(300);

        entityTypeBuilder.Property(u => u.Type)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        entityTypeBuilder.HasMany(u => u.Enrollments)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Email confirmation
        entityTypeBuilder.Property(u => u.IsEmailConfirmed)
            .IsRequired();

        entityTypeBuilder.Property(u => u.EmailConfirmationToken)
            .HasMaxLength(200);
    }
}
