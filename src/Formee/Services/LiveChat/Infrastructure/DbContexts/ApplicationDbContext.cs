using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<AgentEntity> Agents { get; set; }

    public DbSet<SessionEntity> Sessions { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    public DbSet<VisitorEntity> Visitors { get; set; }
}