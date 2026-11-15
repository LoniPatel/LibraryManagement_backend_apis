
using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Authors;

public class DeleteAuthorCommandHandler
    : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<bool> Handle(
        DeleteAuthorCommand command,
        CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(command.Id);

        if (author == null)
        {
            return false;
        }

        // Check whether author has active books
        if (author.Books.Any(book => !book.IsDeleted))
        {
            throw new InvalidOperationException(
                "This author has books assigned. Please delete the books before deleting this author."
            );
        }

        // Soft Delete Author
        author.IsDeleted = true;
        author.DeletedAt = DateTime.Now;
        author.DeletedBy = command.UserId;

        await _authorRepository.UpdateAsync(author);
        await _authorRepository.SaveChangesAsync();

        return true;
    }
}
