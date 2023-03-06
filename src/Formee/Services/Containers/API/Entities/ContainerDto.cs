using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class ContainerDto
{
    [Key]
    public int Id { get; set; }

    public string BsonId { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsDeleted { get; set; } = false;

    public bool? IsModified { get; set; } = false;

    public DateTime? LastModified { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
