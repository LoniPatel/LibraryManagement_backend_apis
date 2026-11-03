using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Authors;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }  
  
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.Where(a => !a.IsDeleted).ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
    }

    // public async Task<bool> IsEmailExistAsync(String email)
    // {
    //     return await _context.Authors.AnyAsync(a => a.Email == email && !a.IsDeleted);
    // }

    // public async Task<bool> IsEmailExistsForOtherAuthorAsync(string email,int authorId)
    // {
    //     return await _context.Authors.AnyAsync(a => a.Email == email && a.Id == authorId && !a.IsDeleted);
    // }

     public async Task<bool> IsEmailExistsAsync(string email)
    {
    return await _context.Authors.AnyAsync(a =>a.Email == email &&!a.IsDeleted);
    }

    public async Task<bool> IsEmailExistsForOtherAuthorAsync(string email,int authorId)
    {
    return await _context.Authors
        .AnyAsync(a =>
            a.Email == email &&
            a.Id != authorId &&
            !a.IsDeleted);
    }
    public async Task<bool> IsNameExistsAsync(string name)
    {
        return await _context.Authors.AnyAsync(a => a.Name == name && !a.IsDeleted);
    }

    public async Task<bool> IsNameExistsForOtherAuthorAsync(string name,int authorId)
    {
        return await _context.Authors.AnyAsync(a => a.Name == name && a.Id == authorId && !a.IsDeleted);
    }

    public async Task AddAsync(Author author)
    {
         await _context.Authors.AddAsync(author);
    }

    public Task UpdateAsync(Author author)
    {
         _context.Authors.Update(author);
         return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}