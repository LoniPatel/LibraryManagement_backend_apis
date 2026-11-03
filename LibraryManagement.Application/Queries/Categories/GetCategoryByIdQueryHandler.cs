
using LibraryManagement.Application.DTOs.Categories;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;

namespace LibraryManagement.Application.Queries.Categories;

public class GetCategoryByIdQueryHandler
    : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDto?>
{
    private readonly ICategoryQueries _categoryQueries;

    public GetCategoryByIdQueryHandler(ICategoryQueries categoryQueries)
    {
        _categoryQueries = categoryQueries;
    }

    public async Task<CategoryResponseDto?> Handle(
        GetCategoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await _categoryQueries.GetCategoryByIdAsync(query.Id);
    }
}
