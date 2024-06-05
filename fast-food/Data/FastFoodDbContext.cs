using fast_food.Models;
using Microsoft.EntityFrameworkCore;

namespace fast_food.Data
{
    public class FastFoodDbContext : DbContext
    {
        public FastFoodDbContext(DbContextOptions<FastFoodDbContext> options) : base(options) { }

        public DbSet<Item> Item { get; set; } = default!;
        
        public DbSet<Cart> Cart { get; set; } = default!;

        public DbSet<CartItem> CartItem { get; set; } = default!;

        public DbSet<Order> Order { get; set; } = default!;

        public DbSet<OrderItem> OrderItem { get; set; } = default!;
    }
}
