using System.ComponentModel.DataAnnotations;

namespace Analytics.Utilities.Dtos.Session;

public class SessionDto
{
    public int Id { get; set; }

    public string DeviceId { get; set; }

    public DateTime LastDeviceHit { get; set; }

    public DateTime? LastModified { get; set; }

    public DateTime CreatedDate { get; set; }
}
