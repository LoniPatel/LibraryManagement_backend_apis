
using LibraryManagement.Application.DTOs.Authors;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;

namespace LibraryManagement.Application.Queries.Authors;

public class GetAuthorByIdQueryHandler
    : IRequestHandler<GetAuthorByIdQuery, AuthorResponseDto?>
{
    private readonly IAuthorQueries _authorQueries;

    public GetAuthorByIdQueryHandler(IAuthorQueries authorQueries)
    {
        _authorQueries = authorQueries;
    }

    public async Task<AuthorResponseDto?> Handle(
        GetAuthorByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await _authorQueries.GetAuthorByIdAsync(query.Id);
    }
}
