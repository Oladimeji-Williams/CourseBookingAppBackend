using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

public static class AuthMapper
{
  public static AuthResponseDto ToAuthResponse(this User user, string token)
  {
    return new AuthResponseDto
    {
      Token = token,
      User = new UserDto
      {
        Id = user.Id,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        PhoneNumber = user.PhoneNumber,
        PhysicalAddress = user.PhysicalAddress,
        Type = user.Type
      }
    };
  }
}
