using LibraryManagement.Application.DTOs.Categories;
using LibraryManagement.Application.Interfaces;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Categories;
using MediatR;
namespace LibraryManagement.Application.Commands.Categories;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand,CategoryResponseDto>
{
    private readonly ICategoryRepository _categoryRepository;

   public CreateCategoryCommandHandler (ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<CategoryResponseDto> Handle(CreateCategoryCommand command,CancellationToken cancellationToken)
    {
        //Check whether Category name already exists
        var categoryExists = await _categoryRepository.IsNameExistsAsync(command.Name);
        if (categoryExists)
        {
            throw new InvalidOperationException("A Category Name already Exists....");
        }
        var category = new Category
        {
            Name = command.Name,
            Description = command.Description,
            CreatedAt = DateTime.Now,
            CreatedBy = command.UserId,
            UpdatedAt = null,
            UpdatedBy = null,
            DeletedAt = null,
            DeletedBy = null,
            IsDeleted = false

        };
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
            CreatedBy = category.CreatedBy,
            UpdatedAt = category.UpdatedAt,
            UpdatedBy = category.UpdatedBy,
            DeletedAt = category.DeletedAt,
            DeletedBy = category.DeletedBy,
            IsDeleted = category.IsDeleted
        };
    }


}
