using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<Enrollment> Enrollments { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}
