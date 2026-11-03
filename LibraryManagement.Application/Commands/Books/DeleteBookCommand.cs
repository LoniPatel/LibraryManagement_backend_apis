using MediatR;

namespace LibraryManagement.Application.Commands.Books;

public class DeleteBookCommand : IRequest<bool>
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DeleteBookCommand(int id, int userId)
    {
        Id = id;
        UserId = userId;
    }
}