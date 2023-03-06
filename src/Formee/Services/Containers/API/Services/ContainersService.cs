﻿using API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services;

public class ContainersService
{
    private readonly IMongoCollection<ContainerEntity> _containerDatabase;
    public ContainersService(IOptions
        <ContainersDatabaseConfiguration> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);

        var mongoDatabase = mongoClient
            .GetDatabase(config.Value.DatabaseName);

        _containerDatabase = mongoDatabase
            .GetCollection<ContainerEntity>(config.Value.CollectionName);
    }

    public async Task<ContainerEntity> GetContainerById(string id)
    {
        var containerFromDb = await 
            _containerDatabase.Find(c => c.Id == id 
                                         && c.IsDeleted != true)
                            .FirstOrDefaultAsync();

        return containerFromDb;
    }
    
    public async Task<List<ContainerEntity>> GetAllContainerByUserIdAsync
        (Guid userId)
    {
        var containerFromDb = await 
            _containerDatabase.Find(c => c.UserId == userId
                                         && c.IsDeleted != true)
                            .ToListAsync();

        return containerFromDb;
    }

    public async Task<ContainerEntity> CreateContainerAsync(
        ContainerEntity container)
    {
       await _containerDatabase.InsertOneAsync(container);

       return container;
    }

    public async Task<bool> UpdateContainerAsync(
        ContainerEntity container)
    {
        container.IsModified = true;
        container.LastModifiedDate = DateTime.Now;

        var result = await _containerDatabase
            .ReplaceOneAsync(c => c.Id == container.Id, container);

        return result.IsAcknowledged;
    }

    public async Task<bool> DeleteContainerAsync(
        string id)
    {
        var containerFromDb = await GetContainerById(id);

        containerFromDb.IsDeleted = true;
        containerFromDb.IsModified = true;
        containerFromDb.LastModifiedDate = DateTime.Now;

        var container = await _containerDatabase
            .ReplaceOneAsync(c => c.Id == id, containerFromDb);

        return container.IsAcknowledged;
    }
}
