using Dapper;
using LibraryManagement.Application.DTOs.Notifications;
using LibraryManagement.Application.Interfaces.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Infrastructure.Queries.Notifications;

public class NotificationQueries : INotificationQueries
{
    private readonly string _connectionString;

    public NotificationQueries(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "DefaultConnection connection string is missing.");
    }

    public async Task<IEnumerable<NotificationResponseDto>>
        GetMyNotificationsAsync(int userId)
    {
        const string sql = """
            SELECT
                Id,
                BookId,
                Message,
                IsRead,
                CreatedAt
            FROM Notifications
            WHERE UserId = @UserId
            ORDER BY CreatedAt DESC;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QueryAsync<NotificationResponseDto>(
            sql,
            new { UserId = userId });
    }
}