using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Interfaces;

namespace CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

public class Course : BaseEntity, IHasImage
{
  public string Title { get; private set; }
  public string Description { get; private set; }
  public double Price { get; private set; }
  public CourseType Type { get; private set; }

  public string? ImgUrl { get; private set; }
  public string? ImgPublicId { get; private set; }

  public bool SoldOut { get; private set; }
  public bool OnSale { get; private set; }

  private readonly List<Enrollment> _enrollments = new();
  public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;

  protected Course() { } // EF Core

  public Course(
      string title,
      string description,
      double price,
      CourseType type)
  {
    Title = title;
    Description = description;
    Price = price;
    Type = type;
    SoldOut = false;
    OnSale = false;
  }

  public void UpdateDetails(
      string title,
      string description,
      double price,
      CourseType type)
  {
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
