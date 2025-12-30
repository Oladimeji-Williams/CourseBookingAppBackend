
namespace CourseBookingAppBackend.src.Application.Commands.Courses;

public sealed record UploadCourseImageCommand(
    int CourseId,
    Stream FileStream,
    string ContentType,
    string FileName
);
