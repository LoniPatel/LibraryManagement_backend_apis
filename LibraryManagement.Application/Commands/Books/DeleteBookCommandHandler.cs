using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Books;

public class DeleteBookCommandHandler
    : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandler(
        IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<bool> Handle(
        DeleteBookCommand command,
        CancellationToken cancellationToken)
    {
        // Get existing book
        var book =
            await _bookRepository.GetByIdAsync(command.Id);

        if (book == null)
        {
            return false;
        }

        // Soft delete
        book.IsDeleted = true;
        book.DeletedAt = DateTime.Now;
        book.DeletedBy = command.UserId;

        await _bookRepository.UpdateAsync(book);
        await _bookRepository.SaveChangesAsync();

        return true;
    }
}