using CourseBookingAppBackend.src.Domain.Entities;

namespace CourseBookingAppBackend.src.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<IEnumerable<User>> GetUsersAsync();

    void Update(User user);
    void Delete(User user);

    Task SaveChangesAsync();
}
