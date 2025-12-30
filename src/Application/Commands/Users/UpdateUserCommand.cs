using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Commands.Users;

public sealed record UpdateUserCommand(
    int TargetUserId,
    int CurrentUserId,
    UserType CurrentUserRole,
    UpdateUserDto Dto
);
