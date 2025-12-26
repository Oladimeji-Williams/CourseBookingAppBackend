namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Security;

public interface IPasswordService
{
    string Hash(string password);
    bool Verify(string hash, string password);
}
