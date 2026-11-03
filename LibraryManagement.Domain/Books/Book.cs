//namespace LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Categories;
using LibraryManagement.Domain.Authors;
namespace LibraryManagement.Domain.Books;
public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime PublishedDate { get; set; }

    //public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }

    //public Author Author { get; set; } = null!;
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public Category Category { get; set; } = null!;
}