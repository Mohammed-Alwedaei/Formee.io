using Links.Utilities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Links.BusinessLogic.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<LinkEntity> Links { get; set; }

    public DbSet<LinkDetailsEntity> LinkDetails { get; set; }

    public DbSet<LinkHitEntity> LinkHit { get; set; }
}
