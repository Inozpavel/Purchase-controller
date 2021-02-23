using Microsoft.EntityFrameworkCore;
using Purchases.Entities;

namespace Purchases.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}