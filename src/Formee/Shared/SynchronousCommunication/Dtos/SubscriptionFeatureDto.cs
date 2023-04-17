namespace SynchronousCommunication.Dtos;

public class SubscriptionFeatureDto : BaseDto
{
    public string? Name { get; set; }

    public int NumberOfContainers { get; set; }

    public int NumberOfForms { get; set; }

    public int NumberOfLinks { get; set; }

    public bool HasLiveChatAccess { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime DeletedDate { get; set; }
}
