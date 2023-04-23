using System.ComponentModel.DataAnnotations;

namespace Identity.BusinessLogic.Dtos;

public class UpsertUserDto
{
    public Guid? Id { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string AuthId { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string? Job { get; set; }

    [Required]
    [MaxLength(2048)]
    public string? Bio { get; set; }

    [Required]
    [Phone]
    [MaxLength(40)]
    public string? PhoneNumber { get; set; }

    [Required]
    public DateTime? BirthDate { get; set; }
}
