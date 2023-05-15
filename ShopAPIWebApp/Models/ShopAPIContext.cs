using Microsoft.EntityFrameworkCore;

namespace ShopAPIWebApp.Models
{
    public class ShopAPIContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public ShopAPIContext(DbContextOptions<ShopAPIContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}
