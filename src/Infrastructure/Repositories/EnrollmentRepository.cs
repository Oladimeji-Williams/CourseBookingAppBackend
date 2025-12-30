using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.Infrastructure.Repositories;

public class EnrollmentRepository(AppDbContext context) : IEnrollmentRepository
{
  private readonly AppDbContext _context = context;

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
