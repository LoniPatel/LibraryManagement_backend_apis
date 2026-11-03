using LibraryManagement.Application.DTOs.Categories;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;
namespace LibraryManagement.Application.Queries.Categories;

public class SearchCategoriesQueryHandler : IRequestHandler<SearchCategoriesQuery, IEnumerable<CategoryResponseDto>>

{
    private readonly ICategoryQueries _categoryQueries;
    public SearchCategoriesQueryHandler(ICategoryQueries categoryQueries)
    {
        _categoryQueries = categoryQueries;
    }

    public async Task<IEnumerable<CategoryResponseDto>> Handle(SearchCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _categoryQueries.SearchCategoriesAsync(request.SearchTerm);
    }
}
