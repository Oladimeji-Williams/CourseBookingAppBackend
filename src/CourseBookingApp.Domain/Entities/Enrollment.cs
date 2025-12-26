namespace CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

public class Enrollment : BaseEntity
{
  public int UserId { get; private set; }
  public int CourseId { get; private set; }
  public bool IsActive { get; private set; }

  // Navigation properties (EF only)
  public User User { get; private set; } = null!;
  public Course Course { get; private set; } = null!;

  protected Enrollment() { } // EF Core

  private Enrollment(int userId, int courseId)
  {
    UserId = userId;
    CourseId = courseId;
    IsActive = true;
  }

  public static Enrollment Create(int userId, int courseId)
  {
    return new Enrollment(userId, courseId);
  }

  public void Cancel()
  {
    if (!IsActive)
      throw new InvalidOperationException("Enrollment already cancelled");

    IsActive = false;
  }
}
