using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Domain.Entities;

namespace CourseBookingAppBackend.src.Application.Mappers;

public static class UserMapper
{
  public static UserDto ToDto(this User user)
  {
    return new UserDto
    {
      Id = user.Id,
      Email = user.Email,
      FirstName = user.FirstName,
      LastName = user.LastName,
      PhoneNumber = user.PhoneNumber,
      PhysicalAddress = user.PhysicalAddress,
      Img = user.ImgUrl,
      ImgPublicId = user.ImgPublicId,
      Type = user.Type,
      Created = user.Created
    };
  }

  public static void MapUpdate(this User user, UpdateUserDto dto)
  {
    user.UpdateProfile(
      dto.FirstName ?? user.FirstName,
      dto.LastName ?? user.LastName,
      dto.PhoneNumber ?? user.PhoneNumber,
      dto.PhysicalAddress ?? user.PhysicalAddress
    );
  }
}
