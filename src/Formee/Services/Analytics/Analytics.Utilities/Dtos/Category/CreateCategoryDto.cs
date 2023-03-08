namespace Analytics.Utilities.Dtos.Category;
public class CreateCategoryDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
