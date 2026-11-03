using LibraryManagement.Application.DTOs.Authors;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Authors;
using MediatR;

namespace LibraryManagement.Application.Commands.Authors;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand,AuthorResponseDto>
{
    private readonly IAuthorRepository _authorRepository;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    public async Task<AuthorResponseDto> Handle(CreateAuthorCommand command,CancellationToken cancellationToken)
    {
        var nameExists = await _authorRepository.IsNameExistsAsync(command.Name);
        if (nameExists)
        {
            throw new InvalidOperationException("An Author with this name already Exists ....");
        }
        var emailExists = await _authorRepository.IsEmailExistsAsync(command.Email);
        if (emailExists)
        {
            throw new InvalidOperationException("An Email with this already Exists ....");
        }
        var author = new Author
        {
           Name = command.Name,
           Email = command.Email,
           CreatedAt = DateTime.Now,
           CreatedBy = command.UserId,
           UpdatedAt = null,
           UpdatedBy = null,
           DeletedAt = null,
           DeletedBy = null,
           IsDeleted = false

        };
        
         await _authorRepository.AddAsync(author);
         await _authorRepository.SaveChangesAsync();

         return new AuthorResponseDto
         {
             Id = author.Id,
             Name = author.Name,
             Email = author.Email,
             CreatedAt = author.CreatedAt,
             CreatedBy = author.CreatedBy,
             UpdatedAt = author.UpdatedAt,
             UpdatedBy = author.UpdatedBy,
             DeletedAt = author.DeletedAt,
             DeletedBy = author.DeletedBy,
             IsDeleted = author.IsDeleted
         };

    }
}