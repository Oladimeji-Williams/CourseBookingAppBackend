using CourseBookingAppBackend.src.Application.Commands.Auth.ConfirmEmail;
using CourseBookingAppBackend.src.Application.Commands.Auth.Login;
using CourseBookingAppBackend.src.Application.Commands.Auth.Register;
using CourseBookingAppBackend.src.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingAppBackend.src.API.Controllers;

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
        var result = await handler.Handle(new LoginCommand(dto.Email, dto.Password));
        return Ok(result);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterDto dto,
        [FromServices] RegisterCommandHandler handler)
    {
        try
        {
            var result = await handler.Handle(new RegisterCommand(dto.Email, dto.Password));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An unexpected error occurred" });
        }
    }

    [HttpPost("confirm-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(
        [FromBody] ConfirmEmailCommand command,
        [FromServices] ConfirmEmailCommandHandler handler)
    {
        try
        {
            await handler.Handle(command);
            return Ok(new { message = "Email confirmed successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An unexpected error occurred" });
        }
    }
}
