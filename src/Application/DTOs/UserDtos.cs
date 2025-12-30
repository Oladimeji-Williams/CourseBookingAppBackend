using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.DTOs;

public class UserDto
{
  public int Id { get; set; }
  public string Email { get; set; } = string.Empty;
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? PhoneNumber { get; set; }
  public string? PhysicalAddress { get; set; }
  public string? Img { get; set; }
  public string? ImgPublicId { get; set; }

  public UserType Type { get; set; } = UserType.Student;
  public DateTime Created { get; set;}
}

public class UpdateUserDto
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? PhoneNumber { get; set; }
  public string? PhysicalAddress { get; set; }
}
public class ChangeRoleDto
{
  public UserType Type { get; set; }
}