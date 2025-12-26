using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;
using CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetUserByIdAsync(int userId)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
  }

  public async Task<IEnumerable<User>> GetUsersAsync()
  {
    return await _context.Users.AsNoTracking().ToListAsync();
  }

  public void Update(User user)
  {
    _context.Users.Update(user);
  }

  public void Delete(User user)
  {
    _context.Users.Remove(user);
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}
