using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.Books;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Security.Claims;
using LibraryManagement.Application.Commands.Authors;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Queries.Books;


namespace LibraryManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMediator _mediator;

    public BooksController(IBookService bookService,IMediator mediator)
    {
        _bookService = bookService;
        _mediator = mediator;
    }

    // GET: api/books
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //var books = await _bookService.GetAllAsync();
        var query = new GetBooksQuery();
        var books = await _mediator.Send(query);

        return Ok(new ApiResponse<IEnumerable<BookResponseDto>>
        {
            Success = true,
            Message = "Books data received successfully",
            Data = books
        });
    }

    // GET: api/books/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        //var book = await _bookService.GetByIdAsync(id);
        var query = new GetBookByIdQuery(id);
        var book = await _mediator.Send(query);

        if (book == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Book not found",
                Data = null
            });
        }

        return Ok(new ApiResponse<BookResponseDto>
        {
            Success = true,
            Message = "Book data received successfully",
            Data = book
        });
    }
    [Authorize(Roles = "Admin,Librarian")]
    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var book = await _bookService.CreateAsync(dto,userId);
        var command = new CreateBookCommand
        {
            Title = dto.Title,
            ISBN = dto.ISBN,
            Price = dto.Price,
            PublishedDate = dto.PublishedDate,
            AuthorIds = dto.AuthorIds,
            CategoryId = dto.CategoryId,
            UserId = userId
        };
        var book = await _mediator.Send(command);

        return StatusCode(201, new ApiResponse<BookResponseDto>
        {
            Success = true,
            Message = "Book created successfully",
            Data = book
        });
    }
    [Authorize(Roles = "Admin,Librarian")]
    // PUT: api/books/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateBookDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var result = await _bookService.UpdateAsync(id, dto, userId);
         var command = new UpdateBookCommand
    {
        Id = id,
        Title = dto.Title,
        ISBN = dto.ISBN,
        Price = dto.Price,
        PublishedDate = dto.PublishedDate,
        AuthorIds = dto.AuthorIds,
        CategoryId = dto.CategoryId,
        UserId = userId
    };

    var result = await _mediator.Send(command);


        if (result == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Book not found",
                Data = null
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Book updated successfully",
            Data = null
        });
    }

    [Authorize(Roles = "Admin")]
    // DELETE: api/books/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var result = await _bookService.DeleteAsync(id, userId);
        var command = new DeleteBookCommand(id,userId);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Book not found",
                Data = null
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Book deleted successfully",
            Data = null
        });
    }
}