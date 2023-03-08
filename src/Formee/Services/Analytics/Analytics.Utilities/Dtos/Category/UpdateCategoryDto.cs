using System.ComponentModel.DataAnnotations;

namespace Analytics.Utilities.Dtos.Category;

public class UpdateCategoryDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public bool IdModified { get; set; } = true;

    public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
}
