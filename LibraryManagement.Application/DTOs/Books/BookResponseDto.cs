// namespace LibraryManagement.Application.DTOs.Books;

// public class BookResponseDto
// {
//     public int Id { get; set; }

//     public string Title { get; set; } = string.Empty;

//     public string ISBN { get; set; } = string.Empty;

//     public decimal Price { get; set; }

//     public DateTime PublishedDate { get; set; }

//     public int AuthorId { get; set; }

//     public string AuthorName { get; set; } = string.Empty;

//     public int CategoryId { get; set; }

//     public string CategoryName { get; set; } = string.Empty;

//     public DateTime CreatedAt { get; set; }

//     public DateTime UpdatedAt { get; set; }
// }

namespace LibraryManagement.Application.DTOs.Books;

public class BookResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTime PublishedDate { get; set; }

   // public AuthorInfoDto Author { get; set; } = null!;
   public List<AuthorInfoDto> Authors { get; set; } = new();

    public CategoryInfoDto Category { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

   
    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}