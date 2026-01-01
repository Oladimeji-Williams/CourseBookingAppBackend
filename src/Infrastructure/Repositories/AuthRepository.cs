using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseBookingAppBackend.src.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
  private readonly AppDbContext _appDbContext;

  public AuthRepository(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }

  public async Task<User?> GetUserByIdAsync(int id)
  {
    return await _appDbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _appDbContext.Users.FirstOrDefaultAsync(s => s.Email == email);
  }

  public async Task AddAsync(User user)
  {
    await _appDbContext.Users.AddAsync(user);
  }

  public async Task SaveChangesAsync()
  {
    await _appDbContext.SaveChangesAsync();
  }
  public async Task<User?> GetByEmailConfirmationTokenAsync(string token)
  {
    return await _appDbContext.Users
        .FirstOrDefaultAsync(u =>
            u.EmailConfirmationToken == token &&
            !u.IsDeleted);
  }

}
