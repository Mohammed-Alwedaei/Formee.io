﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.BusinessLogic.Dtos;

namespace Identity.BusinessLogic.Entities;

public class AvatarEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }

    public bool? IsDeleted { get; set; } = false;

    public DateTime? DeletedDate { get; set; }

    public DateTime UploadedDate { get; set; }

    public static implicit operator AvatarEntity(AvatarDto avatar)
    {
        return new AvatarEntity
        {
            Id = avatar.Id,
            Name = avatar.Name,
            UserId = avatar.UserId,
            IsDeleted = avatar.IsDeleted,
            DeletedDate = avatar.DeletedDate,
            UploadedDate = avatar.UploadedDate,
        };
    }
}
