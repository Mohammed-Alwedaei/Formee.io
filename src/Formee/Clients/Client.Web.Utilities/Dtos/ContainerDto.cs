namespace Client.Web.Utilities.Dtos;

public class ContainerDto
{
    public string Id { get; set; }

    public string? Name { get; set; }

    public Guid UserId { get; set; }

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public bool IsDeleted { get; set; }

    public bool? IsModified { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
