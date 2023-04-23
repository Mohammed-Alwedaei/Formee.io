using Identity.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.BusinessLogic.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }

    public DbSet<UserEntity> User { get; set; }

    public DbSet<AvatarEntity> Avatar { get; set; }

    public DbSet<AccessTokenEntity> AccessToken { get; set; }
}
