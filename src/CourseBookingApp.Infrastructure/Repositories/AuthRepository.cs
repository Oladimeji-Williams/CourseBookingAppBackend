using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;
using CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Repositories;

public class AuthRepository(AppDbContext appDbContext) : IAuthRepository
{
  private readonly AppDbContext _appDbContext = appDbContext;

  public async Task<User> GetUserByIdAsync(int id)
  {
    return await _appDbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
  }

  public async Task SaveChangesAsync()
  {
    await _appDbContext.SaveChangesAsync();
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _appDbContext.Users.FirstOrDefaultAsync(s => s.Email == email);
  }

  public async Task AddAsync(User user)
  {
    await _appDbContext.Users.AddAsync(user);
  }
}
