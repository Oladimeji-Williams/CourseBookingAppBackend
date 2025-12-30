using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Commands.Users;

public sealed record UploadUserImageCommand(
    int TargetUserId,
    int CurrentUserId,
    UserType CurrentUserRole,
    Stream FileStream,
    string ContentType,
    string FileName
);
