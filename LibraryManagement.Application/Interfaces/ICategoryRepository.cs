//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Categories;
namespace LibraryManagement.Application.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();//Get all categories

    Task<Category?> GetByIdAsync(int id);//Get Category By Id

    Task<bool> IsNameExistsAsync(String name);//Check Duplicate Name at create time

    Task<bool> IsNameExistsForOtherCategoryAsync(String name,int categoryId);//Check Duplicate Name at Update time

    Task AddAsync(Category category);//Add Category

    Task UpdateAsync(Category category);//Update Category

    Task SaveChangesAsync();//Save changes to database
}