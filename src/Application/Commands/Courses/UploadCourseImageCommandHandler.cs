using CourseBookingAppBackend.src.Application.Abstractions.Images;
using CourseBookingAppBackend.src.Application.Abstractions.Persistence;

namespace CourseBookingAppBackend.src.Application.Commands.Courses;

public sealed class UploadCourseImageCommandHandler
{
    private readonly ICourseRepository _repo;
    private readonly IImageRepository _images;

    public UploadCourseImageCommandHandler(
        ICourseRepository repo,
        IImageRepository images)
    {
        _repo = repo;
        _images = images;
    }

    public async Task<string?> Handle(UploadCourseImageCommand command)
    {
        var course = await _repo.GetByIdAsync(command.CourseId);
        if (course is null) return null;

        if (!string.IsNullOrEmpty(course.ImgPublicId))
            await _images.DeleteAsync(course.ImgPublicId);

        var safeTitle = course.Title.Replace(" ", "_").ToLower();
        var fileName = $"{course.Id}_{safeTitle}";

        var (url, publicId) = await _images.UploadAsync(
            command.FileStream,
            command.ContentType,
            "course_images",
            fileName
        );

        course.UpdateImage(url, publicId);
        await _repo.SaveChangesAsync();

        return url;
    }
}