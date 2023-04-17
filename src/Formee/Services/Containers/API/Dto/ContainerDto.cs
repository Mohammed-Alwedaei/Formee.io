namespace API.Dto;

public class ContainerDto
{
    public string? Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Description { get; set; } = null!;

    public string? Icon { get; set; }

    public bool? IsModified { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public static implicit operator ContainerDto(ContainerEntity container)
    {
        return new ContainerDto
        {
            Id = container.Id,
            Name = container.Name,
            UserId = container.UserId,
            Description = container.Description,
            Icon = container.Icon,
            IsModified = container.IsModified,
            LastModifiedDate = container.LastModifiedDate,
            CreatedDate = container.CreatedDate,
        };
    }
}

