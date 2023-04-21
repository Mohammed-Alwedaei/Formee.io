using Identity.BusinessLogic.Dtos;
using Identity.BusinessLogic.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace Identity.BusinessLogic.Services.IServices;

public interface IIdentityManager
{
    Task<bool> AssignRoleToUser(AddRoleToUseDto users, string roleId);

    Task<UserDto> GetByAuthIdAsync(string authId);

    Task<List<UserDto>> GetAllByFilterAsync(string filter);

    Task<UserDto> GetByIdAsync(Guid id);

    //Task<bool> RemoveRoleFromUser(AddRoleToUserModel users, string roleId);

    Task<UserDto> CreateAsync(UserEntity user);

    Task<AvatarDto> UploadUserAvatar(IFormFileCollection avatar, Guid userId);
}