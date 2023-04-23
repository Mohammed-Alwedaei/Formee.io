using System.ComponentModel.DataAnnotations;

namespace Identity.BusinessLogic.Entities;

public class AccessTokenEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(512)]
    public string? Token { get; set; }

    [MaxLength(50)]
    public string IssuedFor { get; set; }

    public bool IsExpired { get; set; }

    public DateTime CreatedDate { get; set; }
}
