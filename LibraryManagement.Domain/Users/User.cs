//namespace LibraryManagement.Domain.Entities;
namespace LibraryManagement.Domain.Users;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }
}

public enum UserRole
{
    Admin = 1,
    Librarian = 2,
    Member = 3
}