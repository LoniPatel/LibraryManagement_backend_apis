using MediatR;

namespace LibraryManagement.Application.Commands.Authors;

public class DeleteAuthorCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public DeleteAuthorCommand(int id, int userId)
    {
        Id = id;
        UserId = userId;
    }
}