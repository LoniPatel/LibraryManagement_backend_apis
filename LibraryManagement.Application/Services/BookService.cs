using LibraryManagement.Application.DTOs.Books;
using LibraryManagement.Application.Interfaces;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Books;
using LibraryManagement.Domain.Authors;
using LibraryManagement.Domain.Categories;

namespace LibraryManagement.Application.Services;

public class BookService : IBookService
{
   // private readonly IApplicationDbContext _context;
    private readonly IBookRepository _bookRepository;

    // public BookService(IApplicationDbContext context)
    // {
    //     _context = context;
    // }
    public BookService(IBookRepository bookRepository)
{
    _bookRepository = bookRepository;
}
    // public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    // {
    //     // var books = await _context.Books
    //     //      .Include(b => b.Author)
    //     //      .Include(b => b.Category)
    //     //      .ToListAsync();
    //     var books = await _bookRepository.GetAllAsync();


    //     return books.Select(book => new BookResponseDto
    //     {
    //        Id = book.Id,
    //         Title = book.Title,
    //         ISBN = book.ISBN,
    //         Price = book.Price,
    //         PublishedDate = book.PublishedDate,
    //         //CreatedAt = book.CreatedAt,
    //         //UpdatedAt = book.UpdatedAt,
    //         // Author = new AuthorInfoDto
    //         // {
    //         //     Id = book.Author.Id,
    //         //     Name = book.Author.Name
    //         // },
    //        Authors = book.Authors
    //         .Where(a => !a.IsDeleted)
    //         .Select(a => new AuthorInfoDto
    //         {
    //             Id = a.Id,
    //             Name = a.Name
    //         })
    //         .ToList(),
    //         Category = new CategoryInfoDto
    //         {
    //             Id = book.Category.Id,
    //             Name = book.Category.Name
    //         },
    //         CreatedBy = book.CreatedBy,
    //         UpdatedBy = book.UpdatedBy,
    //         DeletedAt = book.DeletedAt,
    //         DeletedBy = book.DeletedBy,
    //         IsDeleted = book.IsDeleted

    //     });
    // }

//     public async Task<BookResponseDto?> GetByIdAsync(int id)
//     {
//         // var book = await _context.Books
//         //             .Include(b => b.Authors)
//         //             .Include(b => b.Category)
//         //             .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
//         var book = await _bookRepository.GetByIdAsync(id);

//         if (book == null)
//         {
//             return null;
//         }         
//         return new BookResponseDto
//         {
//             Id = book.Id,
//             Title = book.Title,
//             ISBN = book.ISBN,
//             Price = book.Price,
//             PublishedDate = book.PublishedDate,
//             // Author = new AuthorInfoDto
//             // {
//             //     Id = book.Author.Id,
//             //     Name = book.Author.Name
//             // },
//             Authors = book.Authors
//             .Where(a => !a.IsDeleted)
//             .Select(a => new AuthorInfoDto
//             {
//                 Id = a.Id,
//                 Name = a.Name
//             })
//             .ToList(),
//             Category = new CategoryInfoDto
//             {
//                 Id = book.Category.Id,
//                 Name = book.Category.Name
//             },
//             //CreatedAt = book.CreatedAt,
//             //UpdatedAt = book.UpdatedAt
//             CreatedBy = book.CreatedBy,
//             UpdatedBy = book.UpdatedBy,
//             DeletedAt = book.DeletedAt,
//             DeletedBy = book.DeletedBy,
//             IsDeleted = book.IsDeleted
//         };   
//     }


//     public async Task<BookResponseDto> CreateAsync(CreateBookDto dto,int userId)
//     {
       
//         //var authorExist = await _context.Authors.Where(a =>dto.AuthorIds.Contains(a.Id) &&  !a.IsDeleted).ToListAsync();
//         var authorExist = (await _bookRepository.GetAuthorsByIdsAsync(dto.AuthorIds)).ToList();
//         if (authorExist.Count != dto.AuthorIds.Distinct().Count())
//         {
//             throw new KeyNotFoundException("One or more authors were not found or have been deleted.");
//         }
//         //var categoryExist = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId && !c.IsDeleted);
//         var categoryExist = await _bookRepository.CategoryExistsAsync(dto.CategoryId);
//         if (!categoryExist)
//         {
//             throw new KeyNotFoundException("Category not found or has been deleted.");
//         }
//          var isbnExists = await _bookRepository.IsIsbnExistsAsync(dto.ISBN);

//         if (isbnExists)
//         {
//             throw new InvalidOperationException(
//                 "A book with this ISBN already exists.");
//         }
//         var book = new Book
//         {
//             Title = dto.Title,
//             ISBN = dto.ISBN,
//             Price = dto.Price,
//             PublishedDate = dto.PublishedDate,
//             //AuthorId = dto.AuthorId,
//             Authors = authorExist,
//             CategoryId = dto.CategoryId,
//             CreatedAt = DateTime.Now,
//             UpdatedAt = null,
            
//             CreatedBy = userId,
//             UpdatedBy = null,

//             DeletedAt = null,
//             DeletedBy = null,

//             IsDeleted = false
//         };
//         await _bookRepository.AddAsync(book);
//         await _bookRepository.SaveChangesAsync();

//          var createdBook = await _bookRepository
//         .GetByIdAsync(book.Id);
//         if (createdBook == null)
// {
//     throw new KeyNotFoundException(
//         "Created book could not be found.");
// }

//           return new BookResponseDto
//         {
//             Id = createdBook.Id,
//             Title = createdBook.Title,
//             ISBN = createdBook.ISBN,
//             Price = createdBook.Price,
//             PublishedDate = createdBook.PublishedDate,
//             // Author = new AuthorInfoDto
//             // {
//             //     Id = createdBook.Author.Id,
//             //     Name = createdBook.Author.Name
//             // },
//             Authors = createdBook.Authors.Where(a => !a.IsDeleted).Select(a => new AuthorInfoDto
//             {
//                 Id = a.Id,
//                 Name = a.Name
//             }).ToList(),
//             Category = new CategoryInfoDto
//             {
//                 Id = createdBook.Category.Id,
//                 Name = createdBook.Category.Name
//             },
//             CreatedAt = createdBook.CreatedAt,
//             UpdatedAt = createdBook.UpdatedAt,
//             CreatedBy = createdBook.CreatedBy,
//             UpdatedBy = createdBook.UpdatedBy,

//             DeletedAt = createdBook.DeletedAt,
//             DeletedBy = createdBook.DeletedBy,

//             IsDeleted = createdBook.IsDeleted
//         };    
//     }


//      // UPDATE
//     public async Task<bool> UpdateAsync(int id, UpdateBookDto dto,int userId)
//     {
//         var book = await _bookRepository.GetByIdAsync(id);

//         if (book == null)
//         {
//             return false;
//         }
//         var authors = (await _bookRepository
//         .GetAuthorsByIdsAsync(dto.AuthorIds))
//         .ToList();
//         if (authors.Count != dto.AuthorIds.Distinct().Count())
//         {
//             throw new KeyNotFoundException("Author not found or has been deleted.");
//         }
//          var categoryExists = await _bookRepository
//         .CategoryExistsAsync(dto.CategoryId);
//         if (!categoryExists)
//         {
//             throw new KeyNotFoundException("Category not found or has been deleted.");
//         }
//           var isbnExists = await _bookRepository
//         .IsIsbnExistsForOtherBookAsync(dto.ISBN, id);

//             if (isbnExists)
//             {
//                 throw new InvalidOperationException(
//                     "A book with this ISBN already exists.");
//             }


//         book.Title = dto.Title;
//         book.ISBN = dto.ISBN;
//         book.Price = dto.Price;
//         book.PublishedDate = dto.PublishedDate;
//        // book.AuthorId = dto.AuthorId;
//         book.Authors = authors;
//         book.CategoryId = dto.CategoryId;
//         book.UpdatedAt = DateTime.Now;
//         book.UpdatedBy = userId;

//        await _bookRepository.UpdateAsync(book);
//        await _bookRepository.SaveChangesAsync();

//         return true;
//     }
//       // DELETE
//     public async Task<bool> DeleteAsync(int id,int userId)
//     {
//        var book = await _bookRepository.GetByIdAsync(id);

//         if (book == null)
//         {
//             return false;
//         }

//         book.IsDeleted = true;
//         book.DeletedAt = DateTime.Now;
//         book.DeletedBy = userId;

//         //await _context.SaveChangesAsync();
//        await _bookRepository.UpdateAsync(book);
//     await _bookRepository.SaveChangesAsync();
//         return true;
//     }
}    