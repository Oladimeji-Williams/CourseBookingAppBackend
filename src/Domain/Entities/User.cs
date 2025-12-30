
using CourseBookingAppBackend.src.Domain.Enums;
using CourseBookingAppBackend.src.Domain.Interfaces;

namespace CourseBookingAppBackend.src.Domain.Entities;

public class User : BaseEntity, IHasImage
{
  public string Email { get; private set; } = null!;
  public string PasswordHash { get; private set; } = null!;

  public string? FirstName { get; private set; }
  public string? LastName { get; private set; }
  public string? PhoneNumber { get; private set; }
  public string? PhysicalAddress { get; private set; }

  public string? ImgUrl { get; private set; }
  public string? ImgPublicId { get; private set; }

  public bool IsEmailConfirmed { get; private set; }
  public string? EmailConfirmationToken { get; private set; }

  public UserType Type { get; private set; }

  private readonly List<Enrollment> _enrollments = new();
  public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;

  protected User() { } // EF Core

  private User(string email, string passwordHash, UserType type)
  {
    if (string.IsNullOrWhiteSpace(email))
      throw new ArgumentException("Email is required.", nameof(email));

    if (string.IsNullOrWhiteSpace(passwordHash))
      throw new ArgumentException("Password hash is required.", nameof(passwordHash));

    Email = email;
    PasswordHash = passwordHash;
    Type = type;
    IsEmailConfirmed = false;

  }

  public static User Create(string email, string passwordHash)
      => new(email, passwordHash, UserType.Student);

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
    if (string.IsNullOrWhiteSpace(newPasswordHash))
      throw new ArgumentException("Password hash cannot be empty.", nameof(newPasswordHash));

    PasswordHash = newPasswordHash;
  }

  public void ChangeRole(UserType newRole)
  {
    Type = newRole;
  }
  public void SetEmailConfirmationToken(string token)
  {
    EmailConfirmationToken = token;
  }

  public void ConfirmEmail(string token)
  {
    if (IsEmailConfirmed)
      throw new InvalidOperationException("Email already confirmed");

    if (EmailConfirmationToken != token)
      throw new InvalidOperationException("Invalid confirmation token");

    IsEmailConfirmed = true;
    EmailConfirmationToken = null;
  }
}


