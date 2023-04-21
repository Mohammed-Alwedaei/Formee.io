using Subscriptions.BusinessLogic.Models.Orders;
using Subscriptions.BusinessLogic.Models.Subscriptions;
using Subscriptions.BusinessLogic.Models.Users;

namespace Subscriptions.BusinessLogic.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) 
    {
        
    }
    public DbSet<SubscriptionModel> Subscriptions { get; set; }

    public DbSet<SubscriptionFeatureModel> SubscriptionFeatures { get; set; }

    public DbSet<UserSubscriptionModel> UserSubscriptions { get; set; }

    public DbSet<UserModel> Users { get; set; }

    public DbSet<CouponModel> Coupons { get; set; }

    public DbSet<OrderDetailsModel> OrderDetails { get; set; }

    public DbSet<OrderHeaderModel> OrderHeaders { get; set; }
}