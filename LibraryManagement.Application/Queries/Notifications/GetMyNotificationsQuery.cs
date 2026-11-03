using LibraryManagement.Application.DTOs.Notifications;
using MediatR;

namespace LibraryManagement.Application.Queries.Notifications;

public class GetMyNotificationsQuery
    : IRequest<IEnumerable<NotificationResponseDto>>
{
    public int UserId { get; }

    public GetMyNotificationsQuery(int userId)
    {
        UserId = userId;
    }
}