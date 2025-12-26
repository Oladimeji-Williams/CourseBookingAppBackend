using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;

public interface IAuthRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}
