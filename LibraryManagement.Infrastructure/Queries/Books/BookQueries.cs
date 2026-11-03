
using Dapper;
using LibraryManagement.Application.DTOs.Books;
using LibraryManagement.Application.Interfaces.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Infrastructure.Queries.Books;

public class BookQueries : IBookQueries
{
    private readonly string _connectionString;

    public BookQueries(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "DefaultConnection connection string is missing.");
    }

    public async Task<IEnumerable<BookResponseDto>> GetBooksAsync()
    {
        const string sql = """
            SELECT
                b.Id,
                b.Title,
                b.ISBN,
                b.Price,
                b.PublishedDate,
                b.CreatedAt,
                b.UpdatedAt,
                b.CreatedBy,
                b.UpdatedBy,
                b.DeletedAt,
                b.DeletedBy,
                b.IsDeleted,
                c.Id AS CategoryId,
                c.Name AS CategoryName,
                a.Id AS AuthorId,
                a.Name AS AuthorName
            FROM Books b
            INNER JOIN Categories c ON c.Id = b.CategoryId
            LEFT JOIN BookAuthors ba ON ba.BookId = b.Id
            LEFT JOIN Authors a ON a.Id = ba.AuthorId
            WHERE b.IsDeleted = 0
            ORDER BY b.Id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var rows = await connection.QueryAsync<BookRow, AuthorRow, BookRow>(
            sql,
            (book, author) =>
            {
                if (author?.AuthorId != null)
                {
                    book.Authors.Add(new AuthorInfoDto
                    {
                        Id = author.AuthorId.Value,
                        Name = author.AuthorName ?? string.Empty
                    });
                }

                return book;
            },
            splitOn: "AuthorId");

        return MapBooks(rows);
    }

    public async Task<BookResponseDto?> GetBookByIdAsync(int id)
    {
        const string sql = """
            SELECT
                b.Id,
                b.Title,
                b.ISBN,
                b.Price,
                b.PublishedDate,
                b.CreatedAt,
                b.UpdatedAt,
                b.CreatedBy,
                b.UpdatedBy,
                b.DeletedAt,
                b.DeletedBy,
                b.IsDeleted,
                c.Id AS CategoryId,
                c.Name AS CategoryName,
                a.Id AS AuthorId,
                a.Name AS AuthorName
            FROM Books b
            INNER JOIN Categories c ON c.Id = b.CategoryId
            LEFT JOIN BookAuthors ba ON ba.BookId = b.Id
            LEFT JOIN Authors a ON a.Id = ba.AuthorId
            WHERE b.Id = @Id AND b.IsDeleted = 0;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var rows = await connection.QueryAsync<BookRow, AuthorRow, BookRow>(
            sql,
            (book, author) =>
            {
                if (author?.AuthorId != null)
                {
                    book.Authors.Add(new AuthorInfoDto
                    {
                        Id = author.AuthorId.Value,
                        Name = author.AuthorName ?? string.Empty
                    });
                }

                return book;
            },
            new { Id = id },
            splitOn: "AuthorId");

        return MapBooks(rows).SingleOrDefault();
    }

    private static IEnumerable<BookResponseDto> MapBooks(
        IEnumerable<BookRow> rows)
    {
        return rows
            .GroupBy(row => row.Id)
            .Select(group =>
            {
                var book = group.First();

                book.Authors = group
                    .SelectMany(row => row.Authors)
                    .GroupBy(author => author.Id)
                    .Select(authorGroup => authorGroup.First())
                    .ToList();

                return MapBook(book);
            })
            .ToList();
    }

    private static BookResponseDto MapBook(BookRow row)
    {
        return new BookResponseDto
        {
            Id = row.Id,
            Title = row.Title,
            ISBN = row.ISBN,
            Price = row.Price,
            PublishedDate = row.PublishedDate,
            Authors = row.Authors,
            Category = new CategoryInfoDto
            {
                Id = row.CategoryId,
                Name = row.CategoryName
            },
            CreatedAt = row.CreatedAt,
            UpdatedAt = row.UpdatedAt,
            CreatedBy = row.CreatedBy,
            UpdatedBy = row.UpdatedBy,
            DeletedAt = row.DeletedAt,
            DeletedBy = row.DeletedBy,
            IsDeleted = row.IsDeleted
        };
    }

    private sealed class BookRow
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<AuthorInfoDto> Authors { get; set; } = new();
    }

    private sealed class AuthorRow
    {
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }
}
