using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Images;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed class UploadUserImageCommandHandler
{
    private readonly IUserRepository _users;
    private readonly IImageRepository _images; // changed to repository abstraction

    public UploadUserImageCommandHandler(
        IUserRepository users,
        IImageRepository images) // inject repository
    {
        _users = users;
        _images = images;
    }

    public async Task<string> Handle(UploadUserImageCommand command)
    {
        // 1️⃣ Get the user
        var user = await _users.GetUserByIdAsync(command.TargetUserId)
            ?? throw new KeyNotFoundException("User not found");

        // 2️⃣ Authorization check
        if (command.CurrentUserRole != UserType.Admin &&
            command.CurrentUserId != command.TargetUserId)
            throw new UnauthorizedAccessException();

        // 3️⃣ Delete old image if exists
        if (!string.IsNullOrEmpty(user.ImgPublicId))
        {
            await _images.DeleteAsync(user.ImgPublicId);
        }

        // 4️⃣ Upload new image
        var fileName = $"user_{user.Id}";
        var (url, publicId) = await _images.UploadAsync(
            command.FileStream,
            command.ContentType,
            "user_images",
            fileName);

        // 5️⃣ Update user entity
        user.UpdateImage(url, publicId);
        await _users.SaveChangesAsync();

        // 6️⃣ Return URL of the new image
        return url;
    }
}
