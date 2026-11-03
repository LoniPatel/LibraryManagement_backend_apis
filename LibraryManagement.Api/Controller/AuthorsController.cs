using LibraryManagement.Application.DTOs.Authors;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Application.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LibraryManagement.Application.Commands.Authors;
using MediatR;
using LibraryManagement.Application.Queries.Authors;

namespace LibraryManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
   private readonly IMediator _mediator;

    public AuthorsController(IAuthorService authorService,IMediator mediator)
    {
        _authorService = authorService;
        _mediator = mediator;
    }

    // GET: api/authors
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //var authors = await _authorService.GetAllAsync();
        var query = new GetAuthorsQuery();
        var authors = await _mediator.Send(query);

          return Ok(new ApiResponse<IEnumerable<AuthorResponseDto>>
    {
        Success = true,
        Message = "Authors data received successfully",
        Data = authors
    });
    }

    // GET: api/authors/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        //var author = await _authorService.GetByIdAsync(id);
        var query = new GetAuthorByIdQuery(id);
        var author = await _mediator.Send(query);

        if (author == null)
        {
              return NotFound(new ApiResponse<object>
        {
            Success = false,
            Message = "Author not found",
            Data = null
        });
        }

          return Ok(new ApiResponse<AuthorResponseDto>
    {
        Success = true,
        Message = "Author data received successfully",
        Data = author
    });
    }

    [Authorize(Roles = "Admin,Librarian")]
    // POST: api/authors
    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var author = await _authorService.CreateAsync(dto, userId);
        var command = new CreateAuthorCommand
        {
            Name = dto.Name,
            Email = dto.Email,
            UserId = userId
        };
        var author = await _mediator.Send(command);
         return StatusCode(201, new ApiResponse<AuthorResponseDto>
    {
        Success = true,
        Message = "Author created successfully",
        Data = author
    });
    }

    [Authorize(Roles = "Admin,Librarian")]
    // PUT: api/authors/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAuthorDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var result = await _authorService.UpdateAsync(id, dto,userId);
        var command = new UpdateAuthorCommand(id,dto.Name,dto.Email,userId);
        var result = await _mediator.Send(command);

        if (!result)
        {
             return NotFound(new ApiResponse<object>
        {
            Success = false,
            Message = "Author not found",
            Data = null
        });
        }

         return Ok(new ApiResponse<object>
    {
        Success = true,
        Message = "Author updated successfully",
        Data = null
    });
}

    [Authorize(Roles = "Admin")]
    // DELETE: api/authors/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var result = await _authorService.DeleteAsync(id,userId);
        var command = new DeleteAuthorCommand(id,userId);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(new ApiResponse<object>
        {
            Success = false,
            Message = "Author not found",
            Data = null
        });
        }

          return Ok(new ApiResponse<object>
    {
        Success = true,
        Message = "Author deleted successfully",
        Data = null
    });
    }
}