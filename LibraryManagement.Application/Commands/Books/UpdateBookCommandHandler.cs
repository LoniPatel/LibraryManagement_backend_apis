using LibraryManagement.Application.DTOs.Books;
using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Books;

public class UpdateBookCommandHandler
    : IRequestHandler<UpdateBookCommand, BookResponseDto?>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookCommandHandler(
        IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookResponseDto?> Handle(
        UpdateBookCommand command,
        CancellationToken cancellationToken)
    {
        // Get existing book
        var book =
            await _bookRepository.GetByIdAsync(command.Id);

        if (book == null)
        {
            return null;
        }

        // Check duplicate ISBN
        var isbnExists =
            await _bookRepository.IsIsbnExistsForOtherBookAsync(
                command.ISBN,
                command.Id);

        if (isbnExists)
        {
            throw new InvalidOperationException(
                "A book with this ISBN already exists.");
        }

        // Check authors
        var authorsExist =
            await _bookRepository.AuthorsExistAsync(
                command.AuthorIds);

        if (!authorsExist)
        {
            throw new KeyNotFoundException(
                "One or more selected authors were not found.");
        }

        // Check category
        var categoryExists =
            await _bookRepository.CategoryExistsAsync(
                command.CategoryId);

        if (!categoryExists)
        {
            throw new KeyNotFoundException(
                "The selected category was not found.");
        }

        // Get authors
        var authors =
            await _bookRepository.GetAuthorsByIdsAsync(
                command.AuthorIds);

        // Get category
        var category =
            await _bookRepository.GetCategoryByIdAsync(
                command.CategoryId);

        // Update book
        book.Title = command.Title;
        book.ISBN = command.ISBN;
        book.Price = command.Price;
        book.PublishedDate = command.PublishedDate;
        book.CategoryId = command.CategoryId;
        book.Authors = authors.ToList();

        book.UpdatedAt = DateTime.Now;
        book.UpdatedBy = command.UserId;

        await _bookRepository.UpdateAsync(book);
        await _bookRepository.SaveChangesAsync();

        return new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            Price = book.Price,
            PublishedDate = book.PublishedDate,

            Authors = book.Authors
                .Select(author => new AuthorInfoDto
                {
                    Id = author.Id,
                    Name = author.Name
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