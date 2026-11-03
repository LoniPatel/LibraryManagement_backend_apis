
using LibraryManagement.Application.DTOs.Authors;

namespace LibraryManagement.Application.Interfaces.Queries;

public interface IAuthorQueries
{
    Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync();

    Task<AuthorResponseDto?> GetAuthorByIdAsync(int id);
}
