
using LibraryManagement.Application.DTOs.Authors;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;

namespace LibraryManagement.Application.Queries.Authors;

public class GetAuthorsQueryHandler
    : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorResponseDto>>
{
    private readonly IAuthorQueries _authorQueries;

    public GetAuthorsQueryHandler(IAuthorQueries authorQueries)
    {
        _authorQueries = authorQueries;
    }

    public async Task<IEnumerable<AuthorResponseDto>> Handle(
        GetAuthorsQuery query,
        CancellationToken cancellationToken)
    {
        return await _authorQueries.GetAuthorsAsync();
    }
}
