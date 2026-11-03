using LibraryManagement.Application.DTOs.Auth;

namespace LibraryManagement.Application.Interfaces;

public interface IAuthService
{

    Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto);

    Task<LoginResponseDto?> LoginAsync(LoginDto dto); 

}    