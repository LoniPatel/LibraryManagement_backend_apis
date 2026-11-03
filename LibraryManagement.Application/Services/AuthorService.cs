using LibraryManagement.Application.DTOs.Authors;
using LibraryManagement.Application.Interfaces;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Authors;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Services;

public class AuthorService : IAuthorService
{
    //private readonly IApplicationDbContext _context;
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    // public AuthorService(IApplicationDbContext context)
    // {
    //     _context = context;
    // }


//     public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
//     {
//         //var authors = await _context.Authors.ToListAsync();
//     //     var authors = await _context.Authors
//     // .Where(a => !a.IsDeleted)
//     // .ToListAsync();
//     var authors = await _authorRepository.GetAllAsync();

//        return authors.Select(author => new AuthorResponseDto
//         {
//             Id = author.Id,
//             Name = author.Name,
//             Email = author.Email,

//             CreatedAt = author.CreatedAt,
//             UpdatedAt = author.UpdatedAt,

//             CreatedBy = author.CreatedBy,
//             UpdatedBy = author.UpdatedBy,

//             DeletedAt = author.DeletedAt,
//             DeletedBy = author.DeletedBy,

//             IsDeleted = author.IsDeleted
//         });
//  }

//  public async Task<AuthorResponseDto?> GetByIdAsync(int id)
//     {
//         //var author =  await _context.Authors.FindAsync(id);
//     //     var author = await _context.Authors
//     // .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
//     var author = await _authorRepository.GetByIdAsync(id);
//         if (author == null)
//         {
//             return null;
//         }

//        return new AuthorResponseDto
//         {
//             Id = author.Id,
//             Name = author.Name,
//             Email = author.Email,

//             CreatedAt = author.CreatedAt,
//             UpdatedAt = author.UpdatedAt,

//             CreatedBy = author.CreatedBy,
//             UpdatedBy = author.UpdatedBy,

//             DeletedAt = author.DeletedAt,
//             DeletedBy = author.DeletedBy,

//             IsDeleted = author.IsDeleted
//         };
//     }

//  public async Task<AuthorResponseDto> CreateAsync(CreateAuthorDto dto,int userId)
//     {
//          // Check duplicate email
//         // var emailExists = await _context.Authors
//         //     .AnyAsync(a => a.Email == dto.Email);
//         var emailExists = await _authorRepository.IsEmailExistsAsync(dto.Email);
//         if (emailExists)
//         {
//             throw new InvalidOperationException("An author with this email already exists.");
//         }
//         var author = new Author
//         {
//             Name = dto.Name,
//             Email = dto.Email,
//             CreatedAt = DateTime.Now,
//             UpdatedAt = null,
//             CreatedBy = userId,
//             UpdatedBy = null,
//             IsDeleted = false
//         };
//         await _authorRepository.AddAsync(author);
//         await _authorRepository.SaveChangesAsync();

//         //  _context.Authors.Add(author);
//         //  await _context.SaveChangesAsync();


//        return new AuthorResponseDto
//         {
//             Id = author.Id,
//             Name = author.Name,
//             Email = author.Email,

//             CreatedAt = author.CreatedAt,
//             UpdatedAt = author.UpdatedAt,

//             CreatedBy = author.CreatedBy,
//             UpdatedBy = author.UpdatedBy,

//             DeletedAt = author.DeletedAt,
//             DeletedBy = author.DeletedBy,

//             IsDeleted = author.IsDeleted
//         };
//     }   

// public async Task<bool> UpdateAsync(int id, UpdateAuthorDto dto,int userId)
//     {
//         // var author = await _context.Authors.FindAsync(id);
//         var author = await _authorRepository.GetByIdAsync(id);
//         if (author == null)
//         {
//             return false;
//         }
//           // Check duplicate email excluding current author
//         // var emailExists = await _context.Authors
//         //     .AnyAsync(a => a.Email == dto.Email && a.Id != id);
//         var emailExists = await _authorRepository.IsEmailExistsForOtherAuthorAsync(dto.Email, id);
//         if (emailExists)
//         {
//             throw new InvalidOperationException("An author with this email already exists.");
//         }

//         author.Name = dto.Name;
//         author.Email = dto.Email;
//         author.UpdatedAt = DateTime.Now;
//         author.UpdatedBy = userId;
//         //await _context.SaveChangesAsync();
//         await _authorRepository.UpdateAsync(author);
//         await _authorRepository.SaveChangesAsync();
//         return true;

// }    

// public async Task<bool> DeleteAsync(int id,int userId)
//     {
//         // var author = await _context.Authors.FindAsync(id);
//         var author = await _authorRepository.GetByIdAsync(id);
//         if (author == null)
//         {
//             return false;
//         }
//        // _context.Authors.Remove(author);
//        author.IsDeleted = true;
//        author.DeletedBy = userId;
//        author.DeletedAt = DateTime.Now;
//         //await _context.SaveChangesAsync();
//         await _authorRepository.UpdateAsync(author);
//         await _authorRepository.SaveChangesAsync();
//         return true;
//     }    
}