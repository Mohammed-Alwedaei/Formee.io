using System.ComponentModel.DataAnnotations;

namespace Subscriptions.BusinessLogic.Models;

public class SubscriptionFeaturesModel : BaseModel
{
    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string? Name { get; set; }

    public int NumberOfContainers { get; set; }

    public int NumberOfForms { get; set; }

    public int NumberOfLinks { get; set; }

    public bool HasLiveChatAccess { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime DeletedDate { get; set; }

    public static implicit operator SubscriptionFeaturesModel
        (SubscriptionFeatureDto featureDto)
    {
        var featureModel = new SubscriptionFeaturesModel
        {
            Id = featureDto.Id,
            Name = featureDto.Name,
            NumberOfContainers = featureDto.NumberOfContainers,
            NumberOfForms = featureDto.NumberOfForms,
            NumberOfLinks = featureDto.NumberOfLinks,
            HasLiveChatAccess = featureDto.HasLiveChatAccess,
            IsDeleted = featureDto.IsDeleted,
            DeletedDate = featureDto.DeletedDate,
            CreatedDate = featureDto.CreatedDate
        };

        return featureModel;
    }
}
