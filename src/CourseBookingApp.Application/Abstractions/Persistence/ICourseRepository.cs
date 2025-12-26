using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;


public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task AddAsync(Course course);
    Task RemoveAsync(Course course);
    Task SaveChangesAsync();
}
