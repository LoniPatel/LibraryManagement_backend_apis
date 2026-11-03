using System.Net;
using LibraryManagement.Application.DTOs.Categories;
using MediatR;

namespace LibraryManagement.Application.Commands.Categories;

public class CreateCategoryCommand : IRequest<CategoryResponseDto>
{
    public string Name {get; set;} = string.Empty;

    public string Description {get ; set ;} = string.Empty;

    public int UserId {get;set;}
}