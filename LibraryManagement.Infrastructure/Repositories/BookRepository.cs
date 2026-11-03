using LibraryManagement.Application.Interfaces;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Books;
using LibraryManagement.Domain.Authors;
using LibraryManagement.Domain.Categories;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(b => b.Authors)
            .Include(b => b.Category)
            .Where(b => !b.IsDeleted)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Authors)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(
                b => b.Id == id && !b.IsDeleted);
    }

    public async Task<IEnumerable<Author>> GetAuthorsByIdsAsync(List<int> authorIds)
    {
        return await _context.Authors
            .Where(a =>
                authorIds.Contains(a.Id) &&
                !a.IsDeleted)
            .ToListAsync();
    }

    public async Task<bool> AuthorsExistAsync(List<int> authorIds)
    {
        var count = await _context.Authors
            .CountAsync(a =>
                authorIds.Contains(a.Id) &&
                !a.IsDeleted);

        return count == authorIds.Distinct().Count();
    }

    public async Task<bool> CategoryExistsAsync(
        int categoryId)
    {
        return await _context.Categories
            .AnyAsync(c =>
                c.Id == categoryId &&
                !c.IsDeleted);
    }
    public async Task<Category?> GetCategoryByIdAsync(
    int categoryId)
{
    return await _context.Categories
        .FirstOrDefaultAsync(c =>
            c.Id == categoryId &&
            !c.IsDeleted);
}

    public async Task<bool> IsIsbnExistsAsync(
        string isbn)
    {
        return await _context.Books
            .AnyAsync(b =>
                b.ISBN == isbn &&
                !b.IsDeleted);
    }

    public async Task<bool> IsIsbnExistsForOtherBookAsync(
        string isbn,
        int bookId)
    {
        return await _context.Books
            .AnyAsync(b =>
                b.ISBN == isbn &&
                b.Id != bookId &&
                !b.IsDeleted);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}