using Microsoft.EntityFrameworkCore;

namespace AppManager.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<FileManageEntity> FileManageEntities { get; set; }
        public DbSet<ProductImageEntity> ProductImageEntities { get; set; }
        public DbSet<AccountManagerEntity> AccountManagerEntities { get; set; }
        public DbSet<BannerEntity> BannerEntities { get; set; }
        public DbSet<ShopOrderEntity> ShopOrderEntities { get; set; }
        public DbSet<OrderDetailEntity> OrderDetailEntities { get; set; }
        public DbSet<DiscountEntity> DiscountEntities { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCartEntities { get; set; }
        public DbSet<ShippingAddressEntity> ShippingAddressEntities { get; set; }
        public DbSet<CategoryBlogEntity> CategoryBlogEntities { get; set; }
        public DbSet<BlogEntity> BlogEntities { get; set; }
        public DbSet<BlogImageEntity> BlogImageEntities { get; set; }
        public DbSet<BlogTagEntity> BlogTagEntities { get; set; }
        public DbSet<TagEntity> TagEntities { get; set; }
        public DbSet<AccountImageEntity> AccountImageEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<ContactMessageEntity> ContactMessageEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetailEntity>().HasKey(l => new { l.ShopOrderId, l.ProductId });
            modelBuilder.Entity<BlogTagEntity>().HasKey(l => new { l.BlogId, l.TagId });
        }
    }
}