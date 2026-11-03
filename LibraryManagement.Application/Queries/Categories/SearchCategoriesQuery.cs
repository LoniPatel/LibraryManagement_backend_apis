using LibraryManagement.Application.DTOs.Categories;
using MediatR;

namespace LibraryManagement.Application.Queries.Categories;

public class SearchCategoriesQuery : IRequest<IEnumerable<CategoryResponseDto>>

{
   public string SearchTerm {get;set;}=string.Empty; 
}