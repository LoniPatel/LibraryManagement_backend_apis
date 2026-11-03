
using LibraryManagement.Application.DTOs.Categories;

namespace LibraryManagement.Application.Interfaces.Queries;

public interface ICategoryQueries
{
    Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync();

    Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);

    Task<IEnumerable<CategoryResponseDto>> SearchCategoriesAsync(string searchTerm);
}
