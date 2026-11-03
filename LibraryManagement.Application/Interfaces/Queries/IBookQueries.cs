
using LibraryManagement.Application.DTOs.Books;

namespace LibraryManagement.Application.Interfaces.Queries;

public interface IBookQueries
{
    Task<IEnumerable<BookResponseDto>> GetBooksAsync();

    Task<BookResponseDto?> GetBookByIdAsync(int id);
}
