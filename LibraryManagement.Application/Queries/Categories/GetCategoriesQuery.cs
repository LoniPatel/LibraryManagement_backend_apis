using LibraryManagement.Application.DTOs.Categories;
using MediatR;

namespace LibraryManagement.Application.Queries.Categories;

public class GetCategoriesQuery
    : IRequest<IEnumerable<CategoryResponseDto>>
{
}