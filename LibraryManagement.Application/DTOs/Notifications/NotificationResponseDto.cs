
namespace LibraryManagement.Application.DTOs.Notifications;

public class NotificationResponseDto
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }
}
