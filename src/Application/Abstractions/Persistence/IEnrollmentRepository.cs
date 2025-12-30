using CourseBookingAppBackend.src.Domain.Entities;

namespace CourseBookingAppBackend.src.Application.Abstractions.Persistence;

public interface IEnrollmentRepository
{
  Task<Enrollment?> GetEnrollmentAsync(int userId, int courseId);
  Task<IEnumerable<Enrollment>> GetActiveUserEnrollmentsAsync(int userId);
  Task AddEnrollmentAsync(Enrollment enrollment);
  Task SaveChangesAsync();
}
