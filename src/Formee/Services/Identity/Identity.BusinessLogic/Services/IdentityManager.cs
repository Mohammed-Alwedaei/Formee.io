using System.Net.Http.Json;
using Azure.Storage.Blobs;
using Identity.BusinessLogic.Contexts;
using Identity.BusinessLogic.Dtos;
using Identity.BusinessLogic.Entities;
using Identity.BusinessLogic.Exceptions;
using Identity.BusinessLogic.Models;
using Identity.BusinessLogic.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.BusinessLogic.Services;

public class IdentityManager : IIdentityManager
{
    private readonly IOptions<BlobStorageConfiguration> _blobStorage;
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;

    public IdentityManager(IOptions<BlobStorageConfiguration> blobStorage, 
        HttpClient httpClient,
        ApplicationDbContext context)
    {
        _blobStorage = blobStorage;
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<bool> AssignRoleToUser(AddRoleToUseDto users, 
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

    public async Task<UserDto> GetByAuthIdAsync(string authId)
    {
        var userFromDb = await _context.User
            .AsNoTracking()
            .Include(u => u.Avatar)
            .FirstOrDefaultAsync(u => u.AuthId == authId);

        if (string.IsNullOrEmpty(userFromDb?.AuthId))
        {
            return new UserDto();
        }

        return userFromDb;
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var userFromDb = await _context.User
            .AsNoTracking()
            .Include(u => u.Avatar)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (userFromDb != null && userFromDb.Id == Guid.Empty)
        {
            return new UserDto();
        }

        return userFromDb;
    }

    public async Task<UserEntity> CreateAsync(UserEntity user)
    {
        var hasRegistered = await _context.User
            .FirstOrDefaultAsync(u => u.AuthId == user.AuthId);

        if (hasRegistered is not null)
        {
            throw new BadRequestException("User already created");
        }

        var createdUser = await _context.User.AddAsync(user);

        await _context.SaveChangesAsync();

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

        var userFromDb = await _context.User
            .FirstOrDefaultAsync(
                u => u.Id == avatarToCreate.UserId);

        if (userFromDb is not null)
        {
            var oldAvatar = await _context.Avatar
                .FirstOrDefaultAsync(
                    a => a.UserId == avatarToCreate.UserId);

            if (oldAvatar is not null)
            {
                oldAvatar.IsDeleted = true;
                oldAvatar.DeletedDate = avatarToCreate.UploadedDate;

                _context.Avatar.Update(oldAvatar);
                await _context.SaveChangesAsync();
            }

            await _context.Avatar.AddAsync(avatarToCreate);

            await _context.SaveChangesAsync();

            userFromDb.AvatarId = avatarToCreate.Id;

            _context.User.Update(userFromDb);
            await _context.SaveChangesAsync();
            return await Task.FromResult(avatarToCreate);
        }

        throw new NotFoundException("Entity Not found");
    }
}
