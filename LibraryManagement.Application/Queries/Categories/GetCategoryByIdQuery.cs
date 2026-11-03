using LibraryManagement.Application.DTOs.Categories;
using MediatR;

namespace LibraryManagement.Application.Queries.Categories;

public class GetCategoryByIdQuery : IRequest<CategoryResponseDto>
{
    public int Id {get; set;}
    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}