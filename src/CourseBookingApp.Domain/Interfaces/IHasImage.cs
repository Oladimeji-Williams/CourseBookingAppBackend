namespace CourseBookingAppBackend.src.CourseBookingApp.Domain.Interfaces;

public interface IHasImage
{
  string? ImgUrl { get; }
  string? ImgPublicId { get; }
  void UpdateImage(string? imgUrl, string? imgPublicId);
}
