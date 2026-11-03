using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.Authors;

public class UpdateAuthorDto
{
    [Required(ErrorMessage = "Author name is required.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Author name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
    public string Email { get; set; } = string.Empty;
}