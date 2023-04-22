using System.Text.Json.Serialization;

namespace Admins.Web.Dtos.Users;

public class AddRoleToUser
{
    [JsonIgnore]
    public string? RoleId { get; set; }

    public string? UserId { get; set;}
}
