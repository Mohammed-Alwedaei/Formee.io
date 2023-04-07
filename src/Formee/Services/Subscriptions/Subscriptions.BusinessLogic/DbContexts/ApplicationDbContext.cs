namespace Subscriptions.BusinessLogic.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) 
    {
        
    }
    public DbSet<SubscriptionsModel> Subscriptions { get; set; }

    public DbSet<SubscriptionFeaturesModel> SubscriptionFeatures { get; set; }

    public DbSet<UserSubscriptionModel> UserSubscriptions { get; set; }

    public DbSet<UsersModel> Users { get; set; }

    public DbSet<CouponModel> Coupons { get; set; }

    public DbSet<OrderDetailsModel> OrderDetails { get; set; }

    public DbSet<OrderHeaderModel> OrderHeaders { get; set; }
}