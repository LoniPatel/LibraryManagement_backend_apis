using LibraryManagement.Application.DTOs.Authors;
using MediatR;

namespace LibraryManagement.Application.Queries.Authors;

public class GetAuthorByIdQuery
    : IRequest<AuthorResponseDto?>
{
    public int Id { get; set; }

    public GetAuthorByIdQuery(int id)
    {
        Id = id;
    }
}