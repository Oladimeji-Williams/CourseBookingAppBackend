using System.ComponentModel.DataAnnotations;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Interfaces;

namespace CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

public class User : BaseEntity, IHasImage
{
  public string Email { get; private set; }
  public string PasswordHash { get; private set; }

  public string? FirstName { get; private set; }
  public string? LastName { get; private set; }
  public string? PhoneNumber { get; private set; }
  public string? PhysicalAddress { get; private set; }

  public string? ImgUrl { get; private set; }
  public string? ImgPublicId { get; private set; }

  public UserType Type { get; private set; }

  private readonly List<Enrollment> _enrollments = new();
  public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;

  protected User() { } // EF Core

  public User(string email, string passwordHash, UserType type = UserType.Student)
  {
    Email = email;
    PasswordHash = passwordHash;
    Type = type;
  }
  public static User Create(string email, string passwordHash)
  {
    return new User(email, passwordHash, UserType.Student);
  }
  public void UpdateProfile(
      string? firstName,
      string? lastName,
      string? phoneNumber,
      string? physicalAddress)
  {
    FirstName = firstName;
    LastName = lastName;
    PhoneNumber = phoneNumber;
    PhysicalAddress = physicalAddress;
  }
  public void UpdateImage(string? imgUrl, string? imgPublicId)
  {
    ImgUrl = imgUrl;
    ImgPublicId = imgPublicId;
  }

  public void ChangePassword(string newPasswordHash)
  {
    PasswordHash = newPasswordHash;
  }

  public void ChangeRole(UserType type)
  {
    Type = type;
  }
}
