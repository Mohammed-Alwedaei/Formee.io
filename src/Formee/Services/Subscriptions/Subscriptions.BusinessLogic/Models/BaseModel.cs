using System.ComponentModel.DataAnnotations;

namespace Subscriptions.BusinessLogic.Models;

public class BaseModel
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }
}
