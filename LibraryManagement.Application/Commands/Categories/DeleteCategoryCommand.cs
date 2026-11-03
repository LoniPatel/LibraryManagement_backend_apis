//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Categories;
using MediatR;
namespace LibraryManagement.Application.Commands.Categories;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id {get; set;}

    public int UserId {get; set;}

    public DeleteCategoryCommand(int id,int userId)
    {
        Id = id;
        UserId = userId;
    }

}