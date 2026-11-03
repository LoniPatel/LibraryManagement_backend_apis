using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Categories;

public class DeleteCategoryCommandHandler
    : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler (ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand command,CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(command.Id);
        if (category == null)
        {
            return false;
        }
        category.IsDeleted = true;
        category.DeletedAt = DateTime.Now;
        category.DeletedBy = command.UserId;

        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return true;
    }
}