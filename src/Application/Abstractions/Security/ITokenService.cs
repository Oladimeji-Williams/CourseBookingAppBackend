using CourseBookingAppBackend.src.Domain.Entities;

namespace CourseBookingAppBackend.src.Application.Abstractions.Security;

public interface ITokenService
{
    string Generate(User user);
}
