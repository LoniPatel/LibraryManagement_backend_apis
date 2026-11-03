
using LibraryManagement.Application.DTOs.Books;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;

namespace LibraryManagement.Application.Queries.Books;

public class GetBookByIdQueryHandler
    : IRequestHandler<GetBookByIdQuery, BookResponseDto?>
{
    private readonly IBookQueries _bookQueries;

    public GetBookByIdQueryHandler(IBookQueries bookQueries)
    {
        _bookQueries = bookQueries;
    }

    public async Task<BookResponseDto?> Handle(
        GetBookByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await _bookQueries.GetBookByIdAsync(query.Id);
    }
}

// using LibraryManagement.Application.DTOs.Books;
// using LibraryManagement.Application.Interfaces;
// using MediatR;

// namespace LibraryManagement.Application.Queries.Books;

// public class GetBookByIdQueryHandler
//     : IRequestHandler<GetBookByIdQuery, BookResponseDto?>
// {
//     private readonly IBookRepository _bookRepository;

//     public GetBookByIdQueryHandler(
//         IBookRepository bookRepository)
//     {
//         _bookRepository = bookRepository;
//     }

//     public async Task<BookResponseDto?> Handle(
//         GetBookByIdQuery query,
//         CancellationToken cancellationToken)
//     {
//         var book = await _bookRepository.GetByIdAsync(query.Id);

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

//             Authors = book.Authors
//                 .Select(author => new AuthorInfoDto
//                 {
//                     Id = author.Id,
//                     Name = author.Name
//                 })
//                 .ToList(),

//             Category = new CategoryInfoDto
//             {
//                 Id = book.Category.Id,
//                 Name = book.Category.Name
//             },

//             CreatedAt = book.CreatedAt,
//             UpdatedAt = book.UpdatedAt,
//             CreatedBy = book.CreatedBy,
//             UpdatedBy = book.UpdatedBy,
//             DeletedAt = book.DeletedAt,
//             DeletedBy = book.DeletedBy,
//             IsDeleted = book.IsDeleted
//         };
//     }
// }