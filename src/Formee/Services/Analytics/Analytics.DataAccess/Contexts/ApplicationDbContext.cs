namespace Analytics.BusinessLogic.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<SiteEntity> Site { get; set; }

    public DbSet<PageHitEntity> PageHit { get; set; }

    public DbSet<CategoryEntity> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SiteEntity>()
            .HasOne(s => s.Category)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PageHitEntity>()
            .HasOne(s => s.Category)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}