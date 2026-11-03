using LibraryManagement.Application.DTOs.Auth;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagement.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto)
    {
        var result =
            await _authService.RegisterAsync(dto);

        return Ok(new
        {
            success = true,
            message = "User registered successfully.",
            data = result
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        var result =
            await _authService.LoginAsync(dto);

        if (result == null)
        {
            return Unauthorized(new
            {
                success = false,
                message = "Invalid email or password.",
                data = (object?)null
            });
        }

        return Ok(new
        {
            success = true,
            message = "Login successful.",
            data = result
        });
    }
}