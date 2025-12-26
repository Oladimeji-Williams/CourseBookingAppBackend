using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed record UpdateUserCommand(
    int TargetUserId,
    int CurrentUserId,
    UserType CurrentUserRole,
    UpdateUserDto Dto
);
