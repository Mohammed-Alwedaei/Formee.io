﻿using Links.Utilities.Entities;

namespace Links.BusinessLogic.Repositories.IRepository;
public interface ILinkRepository
{
    /// <summary>
    /// Get a short link by id (Primary Key)
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Link Entity</returns>
    Task<LinkEntity> GetLinkByIdAsync(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetUrl"></param>
    /// <returns></returns>
    Task<RedirectEntity> GetRedirectLinkAsync(string targetUrl);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<LinkEntity>> GetAllByUserId(Guid userId);

    /// <summary>
    /// Get a list of short links that match the containerId
    /// </summary>
    /// <param name="containerId"></param>
    /// <returns>A List of Link Entity</returns>
    Task<IReadOnlyList<LinkEntity>> GetAllLinksByContainerIdAsync
        (string containerId);

    /// <summary>
    /// Create a short link 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Link Entity</returns>
    Task<LinkEntity> CreateLinkAsync(LinkEntity entity);

    /// <summary>
    /// Delete a short link that matches the id (Primary Key)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<LinkEntity> DeleteLinkAsync(int id);

    /// <summary>
    /// Delete all short links that match the containerId
    /// </summary>
    /// <param name="containerId"></param>
    /// <returns>A List of Deleted Links</returns>
    Task<List<LinkEntity>> DeleteAllLinksByContainerIdAsync(string containerId);
}
