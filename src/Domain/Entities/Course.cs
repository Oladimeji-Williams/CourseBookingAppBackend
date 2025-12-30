using CourseBookingAppBackend.src.Domain.Enums;
using CourseBookingAppBackend.src.Domain.Interfaces;

namespace CourseBookingAppBackend.src.Domain.Entities;

public class Course : BaseEntity, IHasImage
{
  // EF Core backing fields (null-forgiving)
  public string Title { get; private set; } = null!;
  public string Description { get; private set; } = null!;
  public decimal Price { get; private set; }
  public CourseType Type { get; private set; }

  public string? ImgUrl { get; private set; }
  public string? ImgPublicId { get; private set; }

  public bool SoldOut { get; private set; }
  public bool OnSale { get; private set; }

  private readonly List<Enrollment> _enrollments = new();
  public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;

  // Required by EF Core
  protected Course() { }

  public Course(
      string title,
      string description,
      decimal price,
      CourseType type)
  {
    SetDetails(title, description, price, type);
    SoldOut = false;
    OnSale = false;
  }

  public void UpdateDetails(
      string title,
      string description,
      decimal price,
      CourseType type)
  {
    SetDetails(title, description, price, type);
  }

  private void SetDetails(
      string title,
      string description,
      decimal price,
      CourseType type)
  {
    if (string.IsNullOrWhiteSpace(title))
      throw new ArgumentException("Title cannot be empty.", nameof(title));

    if (string.IsNullOrWhiteSpace(description))
      throw new ArgumentException("Description cannot be empty.", nameof(description));

    if (price < 0)
      throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

    Title = title;
    Description = description;
    Price = price;
    Type = type;
  }

  public void UpdateImage(string? imgUrl, string? imgPublicId)
  {
    ImgUrl = imgUrl;
    ImgPublicId = imgPublicId;
  }

  public void MarkAsSoldOut()
  {
    SoldOut = true;
  }

  public void PutOnSale()
  {
    OnSale = true;
  }

  public void RemoveFromSale()
  {
    OnSale = false;
  }
}
