namespace CourseBookingAppBackend.src.Application.Abstractions.Images;

public interface IImageRepository
{
    /// <summary>
    /// Upload a stream as an image and return URL + public ID
    /// </summary>
    Task<(string Url, string PublicId)> UploadAsync(
        Stream stream,
        string contentType,
        string folder,
        string fileName);

    /// <summary>
    /// Delete an image by its public ID
    /// </summary>
    Task DeleteAsync(string publicId);
}
