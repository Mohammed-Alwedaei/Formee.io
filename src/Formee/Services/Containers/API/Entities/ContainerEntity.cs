using API.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities;
public class ContainerEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Description { get; set; } = null!;

    public string? Icon { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsModified { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public static implicit operator ContainerEntity(ContainerDto container)
    {
        return new ContainerEntity
        {
            Id = container.Id,
            Name = container.Name,
            UserId = container.UserId,
            Description = container.Description,
            Icon = container.Icon,
            IsModified = container.IsModified,
            LastModifiedDate = container.LastModifiedDate,
            CreatedDate = container.CreatedDate,
        };
    }
}
