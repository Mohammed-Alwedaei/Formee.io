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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Polly;
using SynchronousCommunication.HttpClients;

namespace Identity.BusinessLogic.Services;

public class IdentityManager : IIdentityManager
{
    private readonly IOptions<BlobStorageConfiguration> _blobStorage;
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;
    private readonly ISubscriptionsClient _subscriptionsClient;
    private readonly IConfiguration _configuration;

    public IdentityManager(IOptions<BlobStorageConfiguration> blobStorage,
        HttpClient httpClient,
        ApplicationDbContext context,
        ISubscriptionsClient subscriptionsClient, 
        IConfiguration configuration)
    {
        _blobStorage = blobStorage;
        _httpClient = httpClient;
        _context = context;
        _subscriptionsClient = subscriptionsClient;
        _configuration = configuration;
    }

    public async Task<bool> AssignRoleToUser(AddRoleToUseDto users,
        string roleId)
    {
        var response = await _httpClient
            .PostAsJsonAsync($"/api/v2/roles/{roleId}/users", users);

        return response.IsSuccessStatusCode;
    }

    public async Task<UserDto> GetByAuthIdAsync(string authId)
    {
        var userFromDb = await _context.User
            .AsNoTracking()
            .Include(u => u.Avatar)
            .FirstOrDefaultAsync(u => u.AuthId == authId);

        if (string.IsNullOrEmpty(userFromDb?.AuthId)) return new UserDto();

        return userFromDb;
    }

    public async Task<List<UserDto>> GetAllByFilterAsync(string filter)
    {
        var usersFromDb = filter switch
        {
            "active" => await _context.User.AsNoTracking()
                .Include(u => u.Avatar)
                .Where(u => u.IsDeleted != true)
                .ToListAsync(),
            "deleted" => await _context.User.AsNoTracking()
                .Include(u => u.Avatar)
                .Where(u => u.IsDeleted == true)
                .ToListAsync(),
            _ => await _context.User
                .AsNoTracking()
                .Include(u => u.Avatar)
                .ToListAsync()
        };

        return usersFromDb
            .Select<UserEntity, UserDto>(u => u)
            .ToList();
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

    public async Task<TokenDto?> GetTokenAsync()
    {
        _httpClient.BaseAddress = new 
            Uri($"https://{_configuration["Auth0:domain"]}");

        var parameters = new Dictionary<string, string> {
            { "grant_type", _configuration["M2M:Grant_Type"] },
            { "client_id", _configuration["M2M:Client_Id"] },
            { "client_secret", _configuration["M2M:Client_Secret"] },
            { "audience", _configuration["Auth0:Audience"] }
        };

        var content = new FormUrlEncodedContent(parameters.Select(p =>
            new KeyValuePair<string, string>(p.Key, p.Value?.ToString() ?? "")));

        using var request = new HttpRequestMessage(HttpMethod.Post, "oauth/token")
        {
            Content = content
        };

        var response = await _httpClient.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<TokenDto>();
    }

    public async Task<UserDto> CreateAsync(UserEntity user)
    {
        var hasRegistered = await _context.User
            .FirstOrDefaultAsync(u => u.AuthId == user.AuthId);

        if (hasRegistered is not null)
            throw new BadRequestException("User already created");

        user.Id = Guid.NewGuid();

        user.IsDeleted = false;
        user.CreatedDate = DateTime.UtcNow;

        _context.Entry(user).State = EntityState.Modified;

        var defaultSubscription = await _subscriptionsClient
            .GetDefaultSubscription();

        user.SubscriptionId = defaultSubscription.Id;

        var createdUser = await _context.User.AddAsync(user);

        await _context.SaveChangesAsync();

        return createdUser.Entity;
    }

    public async Task<UserDto> UpdateAsync(UpsertUserDto user)
    {
        var oldUser = await _context.User
            .FirstOrDefaultAsync(u => u.Id == user.Id);

        if (oldUser is null)
            return new UserDto();

        //Fields to update
        oldUser.Email = user.Email;
        oldUser.UserName = user.UserName;
        oldUser.FirstName = user.FirstName;
        oldUser.LastName = user.LastName;
        oldUser.Job = user.Job;
        oldUser.Bio = user.Bio;
        oldUser.PhoneNumber = user.PhoneNumber;
        oldUser.BirthDate = user.BirthDate;
        oldUser.IsModified = true;
        oldUser.LastModifiedDate = DateTime.UtcNow;

        _context.User.Update(oldUser);

        await _context.SaveChangesAsync();

        return oldUser;
    }

    public async Task<AvatarDto> UploadUserAvatar(IFormFileCollection avatar, Guid userId)
    {
        var file = avatar.FirstOrDefault();

        var fileName = file.Name;

        var extension = Path.GetExtension(fileName);

        var uniqueFileName = Guid.NewGuid() + extension;

        var stream = file.OpenReadStream();

        var blobClient = new BlobClient
            (_blobStorage.Value.ConnectionString,
                _blobStorage.Value.ContainerName, uniqueFileName);

        await blobClient.UploadAsync(stream);

        var avatarToCreate = new AvatarDto
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

            //Remove the old avatar 
            if (oldAvatar is not null)
            {
                oldAvatar.IsDeleted = true;
                oldAvatar.DeletedDate = avatarToCreate.UploadedDate;

                userFromDb.AvatarId = null;

                _context.User.Update(userFromDb);

                _context.Avatar.Update(oldAvatar);
                await _context.SaveChangesAsync();
            }

            //Attach the new avatar to the user
            var createdAvatar = await _context.Avatar.AddAsync(avatarToCreate);
            await _context.SaveChangesAsync();

            userFromDb.AvatarId = createdAvatar.Entity.Id;

            _context.User.Update(userFromDb);
            await _context.SaveChangesAsync();

            return await Task.FromResult(createdAvatar.Entity);

        }

        throw new NotFoundException("Entity Not found");
    }
}
