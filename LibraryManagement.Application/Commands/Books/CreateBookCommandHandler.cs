
using LibraryManagement.Application.DTOs.Books;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Books;
using LibraryManagement.Domain.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Domain.Users;

namespace LibraryManagement.Application.Commands.Books;

public class CreateBookCommandHandler
    : IRequestHandler<CreateBookCommand, BookResponseDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IApplicationDbContext _context;

    public CreateBookCommandHandler(
        IBookRepository bookRepository,
        IApplicationDbContext context)
    {
        _bookRepository = bookRepository;
        _context = context;
    }

    public async Task<BookResponseDto> Handle(
        CreateBookCommand command,
        CancellationToken cancellationToken)
    {
        var isbnExists =
            await _bookRepository.IsIsbnExistsAsync(command.ISBN);

        if (isbnExists)
        {
            throw new InvalidOperationException(
                "A book with this ISBN already exists.");
        }

        var authorsExist =
            await _bookRepository.AuthorsExistAsync(command.AuthorIds);

        if (!authorsExist)
        {
            throw new KeyNotFoundException(
                "One or more selected authors were not found.");
        }

        var categoryExists =
            await _bookRepository.CategoryExistsAsync(command.CategoryId);

        if (!categoryExists)
        {
            throw new KeyNotFoundException(
                "The selected category was not found.");
        }

        var category =
            await _bookRepository.GetCategoryByIdAsync(command.CategoryId);

        var authors =
            await _bookRepository.GetAuthorsByIdsAsync(command.AuthorIds);

        var book = new Book
        {
            Title = command.Title,
            ISBN = command.ISBN,
            Price = command.Price,
            PublishedDate = command.PublishedDate,
            CategoryId = command.CategoryId,
            Authors = authors.ToList(),

            CreatedAt = DateTime.Now,
            CreatedBy = command.UserId,

            UpdatedAt = null,
            UpdatedBy = null,
            DeletedAt = null,
            DeletedBy = null,
            IsDeleted = false
        };

        await _bookRepository.AddAsync(book);

        // Save the book first so it has a database-generated Id.
        await _bookRepository.SaveChangesAsync();

        var authorNames = string.Join(
            ", ",
            book.Authors.Select(a => a.Name));

        var message =
            $"A new book is available on store, please have a look at {book.Title} written by {authorNames}";

        // Create a separate notification for each registered user.
        var users = await _context.Users
            .AsNoTracking()
            .Where(u => u.Role == UserRole.Member)
            .Select(u => u.Id)
            .ToListAsync(cancellationToken);

        foreach (var userId in users)
        {
            var notification = new Notification
            {
                UserId = userId,
                BookId = book.Id,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Notifications.AddAsync(
                notification,
                cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            Price = book.Price,
            PublishedDate = book.PublishedDate,

            Authors = book.Authors
                .Select(a => new AuthorInfoDto
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToList(),

            Category = new CategoryInfoDto
            {
                Id = category!.Id,
                Name = category.Name
            },

            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt,
            CreatedBy = book.CreatedBy,
            UpdatedBy = book.UpdatedBy,
            DeletedAt = book.DeletedAt,
            DeletedBy = book.DeletedBy,
            IsDeleted = book.IsDeleted
        };
    }
}

// using LibraryManagement.Application.DTOs.Books;
// using LibraryManagement.Application.Interfaces;
// //using LibraryManagement.Domain.Entities;
// using LibraryManagement.Domain.Books;
// using MediatR;

// namespace LibraryManagement.Application.Commands.Books;

// public class CreateBookCommandHandler
//     : IRequestHandler<CreateBookCommand, BookResponseDto>
// {
//     private readonly IBookRepository _bookRepository;

//     public CreateBookCommandHandler(IBookRepository bookRepository)
//     {
//         _bookRepository = bookRepository;
//     }

//     public async Task<BookResponseDto> Handle(
//         CreateBookCommand command,
//         CancellationToken cancellationToken)
//     {
//         // Check duplicate ISBN
//         var isbnExists =
//             await _bookRepository.IsIsbnExistsAsync(command.ISBN);

//         if (isbnExists)
//         {
//             throw new InvalidOperationException(
//                 "A book with this ISBN already exists.");
//         }

//         // Check authors
//         var authorsExist =
//             await _bookRepository.AuthorsExistAsync(command.AuthorIds);

//         if (!authorsExist)
//         {
//             throw new KeyNotFoundException(
//                 "One or more selected authors were not found.");
//         }

//         // Check category
//         var categoryExists =
//             await _bookRepository.CategoryExistsAsync(command.CategoryId);

//         if (!categoryExists)
//         {
//             throw new KeyNotFoundException(
//                 "The selected category was not found.");
//         }
//         var category =
//     await _bookRepository.GetCategoryByIdAsync(command.CategoryId);

//         // Get authors
//         var authors =
//             await _bookRepository.GetAuthorsByIdsAsync(command.AuthorIds);

//         // Create book
//         var book = new Book
//         {
//             Title = command.Title,
//             ISBN = command.ISBN,
//             Price = command.Price,
//             PublishedDate = command.PublishedDate,
//             CategoryId = command.CategoryId,
//             Authors = authors.ToList(),

//             CreatedAt = DateTime.Now,
//             CreatedBy = command.UserId,

//             UpdatedAt = null,
//             UpdatedBy = null,
//             DeletedAt = null,
//             DeletedBy = null,
//             IsDeleted = false
//         };

//         await _bookRepository.AddAsync(book);
//         await _bookRepository.SaveChangesAsync();

//         return new BookResponseDto
// {
//     Id = book.Id,
//     Title = book.Title,
//     ISBN = book.ISBN,
//     Price = book.Price,
//     PublishedDate = book.PublishedDate,

//     Authors = book.Authors
//         .Select(a => new AuthorInfoDto
//         {
//             Id = a.Id,
//             Name = a.Name
//         })
//         .ToList(),

//     // Category = new CategoryInfoDto
//     // {
//     //     Id = book.CategoryId,
//     //     Name = book.Category?.Name ?? string.Empty
//     // },

//      Category = new CategoryInfoDto
// {
//     Id = category!.Id,
//     Name = category.Name
// },
//     CreatedAt = book.CreatedAt,
//     UpdatedAt = book.UpdatedAt,
//     CreatedBy = book.CreatedBy,
//     UpdatedBy = book.UpdatedBy,
//     DeletedAt = book.DeletedAt,
//     DeletedBy = book.DeletedBy,
//     IsDeleted = book.IsDeleted
// };
//     }
// }