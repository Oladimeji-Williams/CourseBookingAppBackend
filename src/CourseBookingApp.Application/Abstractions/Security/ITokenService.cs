using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Security;

public interface ITokenService
{
    string Generate(User user);
}
