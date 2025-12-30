using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.QueryParams;
using CourseBookingAppBackend.src.Domain.Entities;

namespace CourseBookingAppBackend.src.Application.Abstractions.Persistence;

public interface ICourseRepository
{
    Task<PagedResult<CourseDto>> GetAllAsync(CourseQueryParams query);
    Task<Course?> GetByIdAsync(int id);
    Task AddAsync(Course course);
    Task RemoveAsync(Course course);
    Task SaveChangesAsync();
}
