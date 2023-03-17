using System.Net.Http.Json;
using Azure.Storage.Blobs;
using Identity.BusinessLogic.Contexts;
using Identity.BusinessLogic.Entities;
using Identity.BusinessLogic.Exceptions;
using Identity.BusinessLogic.Models;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Identity.BusinessLogic.Services;

public class IdentityManager : IIdentityManager
{
    private readonly IOptions<BlobStorageConfiguration> _blobStorage;
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _db;

    public IdentityManager(IOptions<BlobStorageConfiguration> blobStorage, 
        HttpClient httpClient,
        ApplicationDbContext db)
    {
        _blobStorage = blobStorage;
        _httpClient = httpClient;
        _db = db;
    }

    public async Task<bool> AssignRoleToUser(AddRoleToUserModel users, 
        string roleId)
    {
        var response = await _httpClient
            .PostAsJsonAsync($"/api/v2/roles/{roleId}/users", users);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<UserEntity> CreateAsync(UserEntity user)
    {
        var hasRegistered = await _db.User
            .FirstOrDefaultAsync(u => u.AuthId == user.AuthId);

        if (hasRegistered is not null)
        {
            throw new BadRequestException("User already created");
        }

        var createdUser = await _db.User.AddAsync(user);

        await _db.SaveChangesAsync();

        return createdUser.Entity;
    }

    public async Task<AvatarEntity> UploadUserAvatar(IFormFile file, Guid userId)
    {
        var fileName = file.FileName;

        var extension = Path.GetExtension(fileName);

        var uniqueFileName = Guid.NewGuid() + extension;

        var stream = file.OpenReadStream();

        var blobClient = new BlobClient
            (_blobStorage.Value.ConnectionString,
                _blobStorage.Value.ContainerName, uniqueFileName);

        await blobClient.UploadAsync(stream);

        var avatarToCreate = new AvatarEntity
        {
            UserId = userId,
            Name = uniqueFileName
        };

        var userFromDb = await _db.User
            .FirstOrDefaultAsync(
                u => u.Id == avatarToCreate.UserId);

        if (userFromDb is not null)
        {
            var oldAvatar = await _db.Avatar
                .FirstOrDefaultAsync(
                    a => a.UserId == avatarToCreate.UserId);

            if (oldAvatar is not null)
            {
                oldAvatar.IsDeleted = true;
                oldAvatar.DeletedDate = avatarToCreate.UploadedDate;

                _db.Avatar.Update(oldAvatar);
                await _db.SaveChangesAsync();
            }

            await _db.Avatar.AddAsync(avatarToCreate);

            await _db.SaveChangesAsync();

            userFromDb.AvatarId = avatarToCreate.Id;

            _db.User.Update(userFromDb);
            await _db.SaveChangesAsync();
            return await Task.FromResult(avatarToCreate);
        }

        throw new NotFoundException("Entity Not found");
    }
}
