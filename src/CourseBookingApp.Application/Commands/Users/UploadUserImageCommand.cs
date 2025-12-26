using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed record UploadUserImageCommand(
    int TargetUserId,
    int CurrentUserId,
    UserType CurrentUserRole,
    Stream FileStream,
    string ContentType,
    string FileName
);
