﻿using Links.Utilities.Entities;

namespace Links.BusinessLogic.Repositories.IRepository;

public interface ILinkHitRepository
{
    /// <summary>
    /// Get all link hits by link hit id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A collection of link hits</returns>
    Task<List<LinkHitEntity>> GetAllByLinkIdAsync(int id);

    /// <summary>
    /// Create a link hit 
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    Task CreateAsync(LinkHitEntity hit);
}