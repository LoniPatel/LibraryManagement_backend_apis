using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Categories;

public class UpdateCategoryCommandHandler
    : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(
        UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        // 1. Find category
        var category = await _categoryRepository
            .GetByIdAsync(command.Id);

        if (category == null)
        {
            return false;
        }

        // 2. Check duplicate category name
        // Only check when the name is actually changed
        if (!string.Equals(
                category.Name,
                command.Name,
                StringComparison.OrdinalIgnoreCase))
        {
            var categoryExists =
                await _categoryRepository
                    .IsNameExistsForOtherCategoryAsync(
                        command.Name,
                        command.Id);

            if (categoryExists)
            {
                throw new InvalidOperationException(
                    "A category with this name already exists.");
            }
        }

        // 3. Update category
        category.Name = command.Name;
        category.Description = command.Description;

        // 4. Update audit information
        category.UpdatedAt = DateTime.Now;
        category.UpdatedBy = command.UserId;

        // 5. Save changes
        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return true;
    }
}