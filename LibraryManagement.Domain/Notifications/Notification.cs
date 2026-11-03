namespace LibraryManagement.Domain.Notifications;

public class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}