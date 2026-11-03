
using Dapper;
using LibraryManagement.Application.DTOs.Authors;
using LibraryManagement.Application.Interfaces.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Infrastructure.Queries.Authors;

public class AuthorQueries : IAuthorQueries
{
    private readonly string _connectionString;

    public AuthorQueries(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "DefaultConnection connection string is missing.");
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync()
    {
        const string sql = """
            SELECT
                Id,
                Name,
                Email,
                CreatedAt,
                CreatedBy,
                UpdatedAt,
                UpdatedBy,
                DeletedAt,
                DeletedBy,
                IsDeleted
            FROM Authors
            WHERE IsDeleted = 0
            ORDER BY Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QueryAsync<AuthorResponseDto>(sql);
    }

    public async Task<AuthorResponseDto?> GetAuthorByIdAsync(int id)
    {
        const string sql = """
            SELECT
                Id,
                Name,
                Email,
                CreatedAt,
                CreatedBy,
                UpdatedAt,
                UpdatedBy,
                DeletedAt,
                DeletedBy,
                IsDeleted
            FROM Authors
            WHERE Id = @Id AND IsDeleted = 0;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QuerySingleOrDefaultAsync<AuthorResponseDto>(
            sql,
            new { Id = id });
    }
}
