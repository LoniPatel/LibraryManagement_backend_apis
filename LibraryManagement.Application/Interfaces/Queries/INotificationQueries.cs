using LibraryManagement.Application.DTOs.Notifications;

namespace LibraryManagement.Application.Interfaces.Queries;

public interface INotificationQueries
{
    Task<IEnumerable<NotificationResponseDto>> GetMyNotificationsAsync(int userId);
}