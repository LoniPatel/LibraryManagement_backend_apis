using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Authors;

public class UpdateAuthorCommandHandler
    : IRequestHandler<UpdateAuthorCommand, bool>
{
    private readonly IAuthorRepository _authorRepository;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<bool> Handle(
        UpdateAuthorCommand command,
        CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(command.Id);

        if (author == null)
        {
            return false;
        }

        if (!string.Equals(
                author.Name,
                command.Name,
                StringComparison.OrdinalIgnoreCase))
        {
            var nameExists =
                await _authorRepository
                    .IsNameExistsForOtherAuthorAsync(
                        command.Name,
                        command.Id);

            if (nameExists)
            {
                throw new InvalidOperationException(
                    "An author with this name already exists.");
            }
        }

        if (!string.Equals(
                author.Email,
                command.Email,
                StringComparison.OrdinalIgnoreCase))
        {
            var emailExists =
                await _authorRepository
                    .IsEmailExistsForOtherAuthorAsync(
                        command.Email,
                        command.Id);

            if (emailExists)
            {
                throw new InvalidOperationException(
                    "An author with this email already exists.");
            }
        }

        author.Name = command.Name;
        author.Email = command.Email;
        author.UpdatedAt = DateTime.Now;
        author.UpdatedBy = command.UserId;

        await _authorRepository.UpdateAsync(author);
        await _authorRepository.SaveChangesAsync();

        return true;
    }
}