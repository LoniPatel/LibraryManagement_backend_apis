using Dapper;
using LibraryManagement.Application.DTOs.Categories;
using LibraryManagement.Application.Interfaces.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Infrastructure.Queries.Categories;

public class CategoryQueries : ICategoryQueries
{
    private readonly string _connectionString;

    public CategoryQueries(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "DefaultConnection connection string is missing.");
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync()
    {
        const string sql = """
            SELECT
                Id,
                Name,
                Description,
                CreatedAt,
                UpdatedAt,
                CreatedBy,
                UpdatedBy,
                DeletedAt,
                DeletedBy,
                IsDeleted
            FROM Categories
            WHERE IsDeleted = 0
            ORDER BY Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QueryAsync<CategoryResponseDto>(sql);
    }

    public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
    {
        const string sql = """
            SELECT
                Id,
                Name,
                Description,
                CreatedAt,
                UpdatedAt,
                CreatedBy,
                UpdatedBy,
                DeletedAt,
                DeletedBy,
                IsDeleted
            FROM Categories
            WHERE Id = @Id AND IsDeleted = 0;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        return await connection.QuerySingleOrDefaultAsync<CategoryResponseDto>(
            sql,
            new { Id = id });
    }
   public async Task<IEnumerable<CategoryResponseDto>> SearchCategoriesAsync(string searchTerm)
    {
        const string sql = """ Select Id,Name,Description,CreatedAt,UpdatedAt,CreatedBy,UpdatedBy,DeletedAt,DeletedBy,IsDeleted From categories where IsDeleted = 0 and Name LIKE @searchTerm order by Name """;
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<CategoryResponseDto>(sql,new {SearchTerm = $"%{searchTerm}%"});
    }
}