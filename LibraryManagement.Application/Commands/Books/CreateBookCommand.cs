using LibraryManagement.Application.DTOs.Books;
using MediatR;

namespace LibraryManagement.Application.Commands.Books;

public class CreateBookCommand : IRequest<BookResponseDto>
{
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime PublishedDate { get; set; }

    public List<int> AuthorIds { get; set; } = new();

    public int CategoryId { get; set; }

    public int UserId { get; set; }
}