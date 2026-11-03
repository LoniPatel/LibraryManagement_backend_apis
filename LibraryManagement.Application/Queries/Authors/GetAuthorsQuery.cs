using LibraryManagement.Application.DTOs.Authors;
using MediatR;

namespace LibraryManagement.Application.Queries.Authors;

public class GetAuthorsQuery
    : IRequest<IEnumerable<AuthorResponseDto>>
{
}