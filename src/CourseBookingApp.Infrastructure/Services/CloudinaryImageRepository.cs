using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Images;

namespace CourseBookingAppBackend.src.CourseBookingApp.Infrastructure.Services;

public sealed class CloudinaryImageRepository : IImageRepository
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageRepository(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<(string Url, string PublicId)> UploadAsync(
        Stream stream,
        string contentType,
        string folder,
        string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, stream),
            Folder = folder,
            PublicId = fileName,
            Overwrite = true,
            UseFilename = true,
            UniqueFilename = false
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        return (result.SecureUrl!.ToString(), result.PublicId);
    }

    public async Task DeleteAsync(string publicId)
    {
        await _cloudinary.DestroyAsync(new DeletionParams(publicId)
        {
            Invalidate = true
        });
    }
}
