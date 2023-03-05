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

    public DbSet<FormEntity> Form { get; set; }

    public DbSet<DetailsEntity> Detail { get; set; }

    public DbSet<FieldEntity> Field { get; set; }

    public DbSet<FormResponseEntity> FormResponse { get; set; }

    public DbSet<FormResponseFieldsEntity> FieldsResponseField { get; set; }
}