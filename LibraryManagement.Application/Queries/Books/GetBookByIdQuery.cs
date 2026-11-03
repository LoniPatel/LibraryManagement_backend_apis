using LibraryManagement.Application.DTOs.Books;
using MediatR;

namespace LibraryManagement.Application.Queries.Books;

public class GetBookByIdQuery : IRequest<BookResponseDto?>
{
    public int Id { get; set; }

    public GetBookByIdQuery(int id)
    {
        Id = id;
    }
}