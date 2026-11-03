//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Users;
namespace LibraryManagement.Application.DTOs.Auth;

public class RegisterResponseDto
{
    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}