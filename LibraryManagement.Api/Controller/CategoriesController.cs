using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.Categories;
using LibraryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LibraryManagement.Application.Commands.Categories;
using LibraryManagement.Application.Queries.Categories;
using MediatR;


namespace LibraryManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMediator _mediator;

    public CategoriesController(ICategoryService categoryService,IMediator mediator)
    {
        _categoryService = categoryService;
        _mediator = mediator;
    }

    // GET: api/categories
    // [HttpGet]
    // public async Task<IActionResult> GetAll()
    // {
    //     var categories = await _categoryService.GetAllAsync();

    //     return Ok(new ApiResponse<IEnumerable<CategoryResponseDto>>
    //     {
    //         Success = true,
    //         Message = "Categories data received successfully",
    //         Data = categories
    //     });
    // }
       [HttpGet]
public async Task<IActionResult> GetAll()
{
    var query = new GetCategoriesQuery();

    var categories = await _mediator.Send(query);

    return Ok(new ApiResponse<IEnumerable<CategoryResponseDto>>
    {
        Success = true,
        Message = "Categories data received successfully",
        Data = categories
    });
} 

    // GET: api/categories/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        //var category = await _categoryService.GetByIdAsync(id);
        var query = new GetCategoryByIdQuery(id);
        var category = await _mediator.Send(query);

        if (category == null)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Category not found",
                Data = null
            });
        }

        return Ok(new ApiResponse<CategoryResponseDto>
        {
            Success = true,
            Message = "Category data received successfully",
            Data = category
        });
    }

    [Authorize(Roles = "Admin,Librarian")]
    // POST: api/categories
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new CreateCategoryCommand
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId
        };
        //var category = await _categoryService.CreateAsync(dto, userId);
         var category = await _mediator.Send(command);
        return StatusCode(201, new ApiResponse<CategoryResponseDto>
        {
            Success = true,
            Message = "Category created successfully",
            Data = category
        });
    }

    [Authorize(Roles = "Admin,Librarian")]
    // PUT: api/categories/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var result = await _categoryService.UpdateAsync(id, dto, userId);
        var command = new UpdateCategoryCommand(id,dto.Name,dto.Description,userId);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Category not found",
                Data = null
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Category updated successfully",
            Data = null
        });
    }

    [Authorize(Roles = "Admin")]
    // DELETE: api/categories/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //var result = await _categoryService.DeleteAsync(id, userId);
        var command = new DeleteCategoryCommand(id,userId);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Category not found",
                Data = null
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Category deleted successfully",
            Data = null
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> searchCategories([FromQuery]string searchTerm)
    {
        if(string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest(new
            {
                success = false,
                message = "Search term cannot be empty",
                data = (object?)null
            });
        }
        var result = await _mediator.Send(new SearchCategoriesQuery{SearchTerm = searchTerm});
        return Ok(new {success = true, message = "Search results",data = result});
    }
}