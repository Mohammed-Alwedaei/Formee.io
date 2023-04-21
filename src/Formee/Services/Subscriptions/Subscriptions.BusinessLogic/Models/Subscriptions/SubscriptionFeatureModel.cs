using System.ComponentModel.DataAnnotations;
using Subscriptions.BusinessLogic.Dtos.Subscriptions;

namespace Subscriptions.BusinessLogic.Models.Subscriptions;

public class SubscriptionFeatureModel : BaseModel
{
    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string? Name { get; set; }

    public int NumberOfContainers { get; set; }

    public int NumberOfSites { get; set; }

    public int NumberOfForms { get; set; }

    public int NumberOfLinks { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime DeletedDate { get; set; }
}
