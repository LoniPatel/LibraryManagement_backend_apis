using LibraryManagement.Application.DTOs.Books;
using MediatR;

namespace LibraryManagement.Application.Queries.Books;

public class GetBooksQuery : IRequest<IEnumerable<BookResponseDto>>
{
}   