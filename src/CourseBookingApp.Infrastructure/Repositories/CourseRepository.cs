using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;
using CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
  private readonly AppDbContext _db;

  public CourseRepository(AppDbContext db)
  {
    _db = db;
  }

  public async Task<IEnumerable<Course>> GetAllAsync()
      => await _db.Courses.AsNoTracking().ToListAsync();

  public async Task<Course?> GetByIdAsync(int id)
      => await _db.Courses.FirstOrDefaultAsync(c => c.Id == id);

  public async Task AddAsync(Course course)
      => await _db.Courses.AddAsync(course);

  public async Task RemoveAsync(Course course)
      => _db.Courses.Remove(course);

  public async Task SaveChangesAsync()
      => await _db.SaveChangesAsync();
}
