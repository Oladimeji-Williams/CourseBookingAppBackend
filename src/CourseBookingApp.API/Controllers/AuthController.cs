using CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Auth.Login;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Auth.Register;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingAppBackend.src.CourseBookingApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginDto dto,
        [FromServices] LoginCommandHandler handler)
    {
        var result = await handler.Handle(
            new LoginCommand(dto.Email, dto.Password));

        return result == null
            ? Unauthorized(new { message = "Invalid email or password" })
            : Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterDto dto,
        [FromServices] RegisterCommandHandler handler)
    {
        var result = await handler.Handle(
            new RegisterCommand(dto.Email, dto.Password));

        return Ok(result);
    }
}
