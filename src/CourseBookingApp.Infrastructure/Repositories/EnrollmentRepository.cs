using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;
using CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
  private readonly AppDbContext _context;

  public EnrollmentRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Enrollment?> GetEnrollmentAsync(int userId, int courseId)
  {
    return await _context.Enrollments
      .FirstOrDefaultAsync(e =>
        e.UserId == userId &&
        e.CourseId == courseId);
  }

  public async Task<IEnumerable<Enrollment>> GetActiveUserEnrollmentsAsync(int userId)
  {
    return await _context.Enrollments
      .Where(e => e.UserId == userId && e.IsActive)
      .ToListAsync();
  }

  public async Task AddEnrollmentAsync(Enrollment enrollment)
  {
    await _context.Enrollments.AddAsync(enrollment);
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}
