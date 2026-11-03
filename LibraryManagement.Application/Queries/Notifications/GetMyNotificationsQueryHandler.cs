using LibraryManagement.Application.DTOs.Notifications;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;

namespace LibraryManagement.Application.Queries.Notifications;

public class GetMyNotificationsQueryHandler
    : IRequestHandler<GetMyNotificationsQuery,
        IEnumerable<NotificationResponseDto>>
{
    private readonly INotificationQueries _notificationQueries;

    public GetMyNotificationsQueryHandler(
        INotificationQueries notificationQueries)
    {
        _notificationQueries = notificationQueries;
    }

    public async Task<IEnumerable<NotificationResponseDto>> Handle(
        GetMyNotificationsQuery query,
        CancellationToken cancellationToken)
    {
        return await _notificationQueries.GetMyNotificationsAsync(
            query.UserId);
    }
}