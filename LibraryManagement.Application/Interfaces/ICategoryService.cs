using LibraryManagement.Application.DTOs.Categories;

namespace LibraryManagement.Application.Interfaces;

public interface ICategoryService
{
    //Task<IEnumerable<CategoryResponseDto>> GetAllAsync();

    //Task<CategoryResponseDto?> GetByIdAsync(int id);

    // Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);

    // Task<bool> UpdateAsync(int id, UpdateCategoryDto dto);

    // Task<bool> DeleteAsync(int id);


    //Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto,int userId);

    //Task<bool> UpdateAsync(int id, UpdateCategoryDto dto,int userId);

    Task<bool> DeleteAsync(int id,int userId);
}