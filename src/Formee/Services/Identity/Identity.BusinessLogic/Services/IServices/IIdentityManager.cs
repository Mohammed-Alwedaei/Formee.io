using Identity.BusinessLogic.Entities;
using Identity.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;

namespace Identity.BusinessLogic.Services.IServices;

public interface IIdentityManager
{
    Task<bool> AssignRoleToUser(AddRoleToUserModel users, string roleId);

    //Task<bool> RemoveRoleFromUser(AddRoleToUserModel users, string roleId);

    Task<UserEntity> CreateAsync(UserEntity user);

    Task<AvatarEntity> UploadUserAvatar(IFormFile file, Guid userId);
}