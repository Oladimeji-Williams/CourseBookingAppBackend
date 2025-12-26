using System.Security.Claims;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Enrollments;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Enrollments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingAppBackend.src.CourseBookingApp.API.Controllers;

[ApiController]
[Authorize]
[Route("api/enrollments")]
public class EnrollmentsController : ControllerBase
{
    private readonly EnrollInCourseCommandHandler _enroll;
    private readonly CancelEnrollmentCommandHandler _cancel;
    private readonly GetMyEnrollmentsQueryHandler _getMine;

    public EnrollmentsController(
        EnrollInCourseCommandHandler enroll,
        CancelEnrollmentCommandHandler cancel,
        GetMyEnrollmentsQueryHandler getMine)
    {
        _enroll = enroll;
        _cancel = cancel;
        _getMine = getMine;
    }
    private int UserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("{courseId:int}")]
    public async Task<IActionResult> Enroll(
        int courseId,
        [FromServices] EnrollInCourseCommandHandler handler)
    {
        var result = await handler.Handle(
            new EnrollInCourseCommand(UserId, courseId));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> MyEnrollments(
        [FromServices] GetMyEnrollmentsQueryHandler handler)
    {
        return Ok(await handler.Handle(
            new GetMyEnrollmentsQuery(UserId)));
    }

    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> Cancel(
        int courseId,
        [FromServices] CancelEnrollmentCommandHandler handler)
    {
        await handler.Handle(
            new CancelEnrollmentCommand(UserId, courseId));

        return NoContent();
    }
}
