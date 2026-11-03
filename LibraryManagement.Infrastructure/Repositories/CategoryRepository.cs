using LibraryManagement.Application.Interfaces;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Categories;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    }

    public async Task<bool> IsNameExistsAsync(String name)
    {
        return await _context.Categories.AnyAsync(c => c.Name == name && !c.IsDeleted);
    }

    // public async Task<bool> IsNameExistsForOtherCategoryAsync(String name,int categoryId)
    // {
    //     return await _context.Categories.AnyAsync(c => c.Id == categoryId && c.Name == name && !c.IsDeleted);
    // }
    public async Task<bool> IsNameExistsForOtherCategoryAsync(
    string name,
    int categoryId)
{
    return await _context.Categories
        .AnyAsync(c =>
            c.Name == name &&
            c.Id != categoryId &&
            !c.IsDeleted);
}

    public async Task AddAsync(Category category)
    {
         await _context.Categories.AddAsync(category);
    }

    public Task UpdateAsync(Category category)
    {
         _context.Categories.Update(category);
         return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}