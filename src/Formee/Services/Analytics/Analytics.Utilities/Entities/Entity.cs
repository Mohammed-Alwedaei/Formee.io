using System.ComponentModel.DataAnnotations;

namespace Analytics.Utilities.Entities;

public class Entity : BaseEntity
{
    public bool IsDeleted { get; set; } = false;

    public bool IdModified { get; set; } = false;

    public DateTime? DeletedDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }
}