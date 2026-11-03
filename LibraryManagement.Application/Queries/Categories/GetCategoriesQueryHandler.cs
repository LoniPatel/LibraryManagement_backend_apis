using LibraryManagement.Application.DTOs.Categories;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;

namespace LibraryManagement.Application.Queries.Categories;

public class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryResponseDto>>
{
    private readonly ICategoryQueries _categoryQueries;

    public GetCategoriesQueryHandler(ICategoryQueries categoryQueries)
    {
        _categoryQueries = categoryQueries;
    }

    public async Task<IEnumerable<CategoryResponseDto>> Handle(
        GetCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        return await _categoryQueries.GetCategoriesAsync();
    }
}