namespace CourseBookingAppBackend.src.Domain.Entities;

public class Enrollment : BaseEntity
{
  public int UserId { get; private set; }
  public int CourseId { get; private set; }
  public bool IsActive { get; private set; }

  // Navigation properties (EF Core only)
  public User User { get; private set; } = null!;
  public Course Course { get; private set; } = null!;

  // Required by EF Core
  protected Enrollment() { }

  private Enrollment(int userId, int courseId)
  {
    if (userId <= 0)
      throw new ArgumentOutOfRangeException(nameof(userId));

    if (courseId <= 0)
      throw new ArgumentOutOfRangeException(nameof(courseId));

    UserId = userId;
    CourseId = courseId;
    IsActive = true;
  }

  public static Enrollment Create(int userId, int courseId)
      => new(userId, courseId);

  public void Cancel()
  {
    if (!IsActive)
      throw new InvalidOperationException("Enrollment is already inactive.");

    IsActive = false;
  }
}
