using LibraryManagement.Application.DTOs.Categories;
using MediatR;

namespace LibraryManagement.Application.Commands.Categories;
public class UpdateCategoryCommand : IRequest<bool>
{
    public int Id {get ; set ;}

    public string Name {get; set;} = string.Empty;

    public string Description {get; set;} = string.Empty;

    public int UserId {get; set;}

    public UpdateCategoryCommand (int id,string name,string description,int userId)
    {
        Id = id;
        Name = name;
        Description = description;
        UserId = userId;

    }
}