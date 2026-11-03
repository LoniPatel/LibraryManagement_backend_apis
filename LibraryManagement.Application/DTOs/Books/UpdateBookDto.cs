using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs.Books;

public class UpdateBookDto
{
      [Required(ErrorMessage = "Book title is required.")]
    [StringLength(200, MinimumLength = 2,
        ErrorMessage = "Book title must be between 2 and 200 characters.")]
    public string Title { get; set; } = string.Empty;

     [Required(ErrorMessage = "ISBN is required.")]
    [StringLength(20, MinimumLength = 10,
        ErrorMessage = "ISBN must be between 10 and 20 characters.")]
    public string ISBN { get; set; } = string.Empty;

     [Range(0.01, 1000000,
        ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

     [Required(ErrorMessage = "Published date is required.")]
    public DateTime PublishedDate { get; set; }

  [Required(ErrorMessage = "At least one author is required.")]
[MinLength(1, ErrorMessage = "At least one author must be selected.")]
public List<int> AuthorIds { get; set; } = new();

    [Range(1, int.MaxValue,
        ErrorMessage = "A valid CategoryId is required.")]
    public int CategoryId { get; set; }
}