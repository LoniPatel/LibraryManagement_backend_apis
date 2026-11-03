using LibraryManagement.Application.DTOs.Categories;
using LibraryManagement.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Services;

public class CategoryService : ICategoryService
{
    //private readonly IApplicationDbContext _context;
    private readonly ICategoryRepository _categoryRepository;

    // public CategoryService(IApplicationDbContext context)
    // {
    //     _context = context;
    // }

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;   
    }

    // public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    // {
    //     //var categories = await _context.Categories.ToListAsync();
    //     // var categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
    //     var categories = await _categoryRepository.GetAllAsync();
    //     return categories.Select(category  => new CategoryResponseDto
    //     {
    //         Id = category .Id,
    //         Name = category .Name,
    //         Description = category .Description,
    //         CreatedAt = category .CreatedAt,
    //         UpdatedAt = category .UpdatedAt,
    //         CreatedBy = category.CreatedBy,
    //         UpdatedBy = category.UpdatedBy,
    //         DeletedAt = category.DeletedAt,
    //         DeletedBy = category.DeletedBy,
    //         IsDeleted = category.IsDeleted

    //     });
    // }

    // public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    // {
    //    // var category = await _context.Categories.FindAsync(id);
    // //     var category = await _context.Categories
    // // .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    // var category = await _categoryRepository.GetByIdAsync(id);
    //     if (category == null)
    //     {
    //         return null;
    //     }
    //     return new CategoryResponseDto
    //     {
    //         Id = category.Id,
    //         Name = category.Name,
    //         Description = category.Description,
    //         CreatedAt = category.CreatedAt,
    //         UpdatedAt = category.UpdatedAt,
    //         CreatedBy = category.CreatedBy,
    //         UpdatedBy = category.UpdatedBy,
    //         DeletedAt = category.DeletedAt,
    //         DeletedBy = category.DeletedBy,
    //         IsDeleted = category.IsDeleted
    //     };
    // }


    // public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto,int userId)
    // {
    //     // Check duplicate category name
    //     // var categoryExists = await _context.Categories
    //     //     .AnyAsync(c => c.Name == dto.Name);
    //     var categoryExists = await _categoryRepository.IsNameExistsAsync(dto.Name);

    //     if (categoryExists)
    //     {
    //         throw new InvalidOperationException("A category with this name already exists.");
    //     }
    //     var category = new Domain.Entities.Category
    //     {
    //         Name = dto.Name,
    //         Description = dto.Description,
    //         CreatedAt = DateTime.Now,
    //         UpdatedAt = null,
    //         CreatedBy = userId,
    //         UpdatedBy = null,
    //         IsDeleted = false
    //     };
    //     // _context.Categories.Add(category);
    //     // await _context.SaveChangesAsync();
    //     await _categoryRepository.AddAsync(category);
    //     await _categoryRepository.SaveChangesAsync();
    //     return new CategoryResponseDto
    //     {
    //         Id = category.Id,
    //         Name = category.Name,
    //         Description = category.Description,
    //         CreatedAt = category.CreatedAt,
    //         UpdatedAt = category.UpdatedAt,
    //         CreatedBy = category.CreatedBy,
    //         UpdatedBy = category.UpdatedBy,
    //         DeletedAt = category.DeletedAt,
    //         DeletedBy = category.DeletedBy,
    //         IsDeleted = category.IsDeleted
    //     };
    // }

    public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto,int userId)
    {
        // var category = await _context.Categories.FindAsync(id);
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return false;
        }
            // Check duplicate category name
            // Exclude the current category
            // var categoryExists = await _context.Categories
            //     .AnyAsync(c => c.Name == dto.Name && c.Id != id);
            var categoryExists = await _categoryRepository.IsNameExistsForOtherCategoryAsync(dto.Name,id);
            if (categoryExists)
            {
                throw new InvalidOperationException("A category with this name already exists.");
            }

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.UpdatedAt = DateTime.Now;
        category.UpdatedBy = userId;
        //await _context.SaveChangesAsync();
        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id,int userId)
    {
       // var category = await _context.Categories.FindAsync(id);
       var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return false;
        }
       // _context.Categories.Remove(category);
        category.IsDeleted = true;
        category.DeletedAt = DateTime.Now;
        category.DeletedBy = userId;
        //await _context.SaveChangesAsync();
        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return true;
    }
}