using Microsoft.EntityFrameworkCore;
using Purchases.Api.Entities;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable NotNullMemberIsNotInitialized

namespace Purchases.Api.Data
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