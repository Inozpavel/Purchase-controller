using Microsoft.EntityFrameworkCore;
using Purchases.Entities;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable NotNullMemberIsNotInitialized

namespace Purchases.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
    }
}