using LibraryManagement.Application.DTOs.Authors;
using LibraryManagement.Domain.Authors;
using MediatR;

namespace LibraryManagement.Application.Commands.Authors;
public class CreateAuthorCommand : IRequest<AuthorResponseDto>
{
    public string Name {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public int UserId {get; set;}

}