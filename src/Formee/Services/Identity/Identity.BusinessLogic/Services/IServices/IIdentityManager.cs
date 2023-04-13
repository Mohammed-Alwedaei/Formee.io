using Identity.BusinessLogic.Dtos;
using Identity.BusinessLogic.Entities;
using Microsoft.AspNetCore.Http;

namespace Identity.BusinessLogic.Services.IServices;

public interface IIdentityManager
{
    Task<bool> AssignRoleToUser(AddRoleToUseDto users, string roleId);

    Task<UserDto> GetByAuthIdAsync(string authId);

    Task<UserDto> GetByIdAsync(Guid id);

    //Task<bool> RemoveRoleFromUser(AddRoleToUserModel users, string roleId);

    Task<UserEntity> CreateAsync(UserEntity user);

    Task<AvatarEntity> UploadUserAvatar(IFormFile file, Guid userId);
}