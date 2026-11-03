using MediatR;

namespace LibraryManagement.Application.Commands.Authors;

public class UpdateAuthorCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int UserId { get; set; }

    public UpdateAuthorCommand(
        int id,
        string name,
        string email,
        int userId)
    {
        Id = id;
        Name = name;
        Email = email;
        UserId = userId;
    }
}