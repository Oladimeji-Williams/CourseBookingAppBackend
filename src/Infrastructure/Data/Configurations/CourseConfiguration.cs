using CourseBookingAppBackend.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseBookingAppBackend.src.Infrastructure.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
  public void Configure(EntityTypeBuilder<Course> entityTypeBuilder)
  {
    entityTypeBuilder.ToTable("Courses");

    entityTypeBuilder.HasKey(c => c.Id);

    entityTypeBuilder.Property(c => c.Title)
        .HasMaxLength(200)
        .IsRequired();

    entityTypeBuilder.Property(c => c.Description)
        .HasMaxLength(2000);

    entityTypeBuilder.Property(c => c.Price)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    entityTypeBuilder.Property(c => c.Type)
        .HasConversion<string>()
        .IsRequired();

    entityTypeBuilder.HasIndex(c => c.IsDeleted);

    // Soft-delete filter
    entityTypeBuilder.HasQueryFilter(c => !c.IsDeleted);

    // Relationships
    entityTypeBuilder.HasMany(c => c.Enrollments)
        .WithOne(e => e.Course)
        .HasForeignKey(e => e.CourseId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
