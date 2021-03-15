using Microsoft.EntityFrameworkCore;
using Stores.Api.Entities;

namespace Stores.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<StoreCategory> StoreCategories { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ProductReceiptInformation> ProductsReceiptInformation { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<CustomCategory> CustomCategories { get; set; }

        public DbSet<CustomCategoryForProduct> CustomCategoriesForProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomCategoryForProduct>().HasKey(x => new
            {
                x.CustomCategoryId,
                x.ProductReceiptInformationId
            });
        }
    }
}