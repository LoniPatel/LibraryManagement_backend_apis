namespace LibraryManagement.Application.DTOs.Categories;

public class CategoryResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

      public int CreatedBy {get; set;}

   
    public int? UpdatedBy {get; set; }

    public DateTime? DeletedAt { get; set;}

    public int? DeletedBy {get; set;}

    public bool IsDeleted {get; set; }
}