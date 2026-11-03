//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Books;
using LibraryManagement.Domain.Authors;
using LibraryManagement.Domain.Categories;
namespace LibraryManagement.Application.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task<IEnumerable<Author>> GetAuthorsByIdsAsync(List<int> authorIds);

    Task<bool> AuthorsExistAsync(List<int> authorIds);

    Task<bool> CategoryExistsAsync(int categoryId);

    Task<Category?> GetCategoryByIdAsync(int categoryId);

    Task<bool> IsIsbnExistsAsync(string isbn);

    Task<bool> IsIsbnExistsForOtherBookAsync(string isbn,int bookId);

    Task AddAsync(Book book);

    Task UpdateAsync(Book book);

    Task SaveChangesAsync();
}