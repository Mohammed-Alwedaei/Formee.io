using System.ComponentModel.DataAnnotations;

namespace Client.Web.Utilities.Dtos;
public class BaseDto
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }
}
