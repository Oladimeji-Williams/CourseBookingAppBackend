using CourseBookingAppBackend.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseBookingAppBackend.src.Infrastructure.Data.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
  public void Configure(EntityTypeBuilder<Enrollment> entityTypeBuilder)
  {
    entityTypeBuilder.ToTable("Enrollments");

    entityTypeBuilder.HasKey(e => e.Id);

    entityTypeBuilder.HasIndex(e => new { e.UserId, e.CourseId })
        .IsUnique()
        .HasFilter("[IsActive] = 1"); // Prevent duplicate active enrollment

    entityTypeBuilder.Property(e => e.IsActive)
        .HasDefaultValue(true);

    entityTypeBuilder.HasOne(e => e.User)
        .WithMany(u => u.Enrollments)
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    entityTypeBuilder.HasOne(e => e.Course)
        .WithMany(c => c.Enrollments)
        .HasForeignKey(e => e.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

    // Optional: filter enrollments of soft-deleted courses
    entityTypeBuilder.HasQueryFilter(e => !e.Course.IsDeleted);
  }
}
