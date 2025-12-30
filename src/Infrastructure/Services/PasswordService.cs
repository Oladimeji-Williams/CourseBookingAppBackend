using CourseBookingAppBackend.src.Application.Abstractions.Security;
using CourseBookingAppBackend.src.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CourseBookingAppBackend.src.Infrastructure.Services;

public class PasswordService : IPasswordService
{
  private readonly PasswordHasher<User> _hasher = new();

  public string Hash(string password)
      => _hasher.HashPassword(null!, password);

  public bool Verify(string hash, string password)
      => _hasher.VerifyHashedPassword(null!, hash, password)
         != PasswordVerificationResult.Failed;
}
