using System.ComponentModel.DataAnnotations;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Users;
namespace LibraryManagement.Application.DTOs.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name {get; set;} = string.Empty;


    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email {get; set;} = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password {get; set;} = string.Empty;

    [Required(ErrorMessage = "Role is required.")]
    [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid role.")]
    public UserRole Role { get; set; }

}
