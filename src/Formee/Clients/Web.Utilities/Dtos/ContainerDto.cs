using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos;

public class ContainerDto
{
    public string? Id { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MinLength(10)]
    [MaxLength(2048)]
    public string? Description { get; set; }

    [Required]
    public string? Icon { get; set; }

    public bool IsDeleted { get; set; }

    public bool? IsModified { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
