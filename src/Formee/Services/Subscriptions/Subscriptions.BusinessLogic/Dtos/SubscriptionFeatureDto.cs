namespace Subscriptions.BusinessLogic.Dtos;

public class SubscriptionFeatureDto : BaseDto
{
    public string? Name { get; set; }

    public int NumberOfContainers { get; set; }

    public int NumberOfForms { get; set; }

    public int NumberOfLinks { get; set; }

    public bool HasLiveChatAccess { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime DeletedDate { get; set; }

    public static implicit operator SubscriptionFeatureDto
        (SubscriptionFeaturesModel featureModel)
    {
        var featureDto = new SubscriptionFeatureDto
        {
            Id = featureModel.Id,
            Name = featureModel.Name,
            NumberOfContainers = featureModel.NumberOfContainers,
            NumberOfForms = featureModel.NumberOfForms,
            NumberOfLinks = featureModel.NumberOfLinks,
            HasLiveChatAccess = featureModel.HasLiveChatAccess,
            IsDeleted = featureModel.IsDeleted,
            DeletedDate = featureModel.DeletedDate,
            CreatedDate = featureModel.CreatedDate
        };

        return featureDto;
    }
}
