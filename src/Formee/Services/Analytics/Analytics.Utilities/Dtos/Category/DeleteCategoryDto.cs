namespace Analytics.Utilities.Dtos.Category;

public class DeleteCategoryDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid UserId { get; set; }

    public bool IdModified { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public bool IsDeleted { get; set; } 

    public DateTime? DeletedDate { get; set; }
}
