﻿using fast_food.Areas.Identity.Data;
using fast_food.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fast_food.Areas.Identity.Data;

public class FastFoodDb : IdentityDbContext<FastFoodUser>
{
    public FastFoodDb(DbContextOptions<FastFoodDb> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Item> Item { get; set; } = default!;
    public DbSet<Cart> Cart { get; set; } = default!;
    public DbSet<CartItem> CartItems { get; set; } = default!;
    public DbSet<Order> Order { get; set; } = default!;
}
