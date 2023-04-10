using System.ComponentModel.DataAnnotations;

namespace Analytics.Utilities.Entities;

public class SessionEntity : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string DeviceId { get; set; }

    public DateTime LastDeviceHit { get; set; } = DateTime.UtcNow;

    public DateTime? LastModified { get; set; } 
}
