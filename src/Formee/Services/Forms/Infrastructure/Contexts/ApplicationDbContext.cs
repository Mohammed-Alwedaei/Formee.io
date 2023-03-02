using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

/// <summary>
/// ApplicationDbContext is the core and only database context
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<FormEntity> Forms { get; set; }

    public DbSet<DetailsEntity> Details { get; set; }

    public DbSet<FieldEntity> Fields { get; set; }

    public DbSet<FieldsWarehouseEntity> FieldsWarehouse { get; set; }
}