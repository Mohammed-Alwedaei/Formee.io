using System.ComponentModel.DataAnnotations;
using Subscriptions.BusinessLogic.Dtos.Subscriptions;

namespace Subscriptions.BusinessLogic.Models.Subscriptions;

public class SubscriptionModel : BaseModel
{
    [Required]
    [MaxLength(255)]
    public string AdminEmail { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(2048)]
    public string? Description { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, 1000)]
    public decimal AnnualPrice { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public int SubscriptionFeaturesId { get; set; }

    public SubscriptionFeatureModel? SubscriptionFeatures { get; set; }
}
