
using LibraryManagement.Application.DTOs.Books;
using LibraryManagement.Application.Interfaces.Queries;
using MediatR;

namespace LibraryManagement.Application.Queries.Books;

public class GetBooksQueryHandler
    : IRequestHandler<GetBooksQuery, IEnumerable<BookResponseDto>>
{
    private readonly IBookQueries _bookQueries;

    public GetBooksQueryHandler(IBookQueries bookQueries)
    {
        _bookQueries = bookQueries;
    }

    public async Task<IEnumerable<BookResponseDto>> Handle(
        GetBooksQuery query,
        CancellationToken cancellationToken)
    {
        return await _bookQueries.GetBooksAsync();
    }
}

// using LibraryManagement.Application.DTOs.Books;
// using LibraryManagement.Application.Interfaces;
// using MediatR;

// namespace LibraryManagement.Application.Queries.Books;

// public class GetBooksQueryHandler
//     : IRequestHandler<GetBooksQuery, IEnumerable<BookResponseDto>>
// {
//     private readonly IBookRepository _bookRepository;

//     public GetBooksQueryHandler(
//         IBookRepository bookRepository)
//     {
//         _bookRepository = bookRepository;
//     }

//     public async Task<IEnumerable<BookResponseDto>> Handle(
//         GetBooksQuery query,
//         CancellationToken cancellationToken)
//     {
//         var books = await _bookRepository.GetAllAsync();

//         return books.Select(book => new BookResponseDto
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
//         });
//     }
// }