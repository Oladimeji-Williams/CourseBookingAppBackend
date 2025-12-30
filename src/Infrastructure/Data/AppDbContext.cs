using CourseBookingAppBackend.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<Enrollment> Enrollments { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Apply all configurations
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}
