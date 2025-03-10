using Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Authentication.Entity;
using Model.Order.Entity;
using Model.Product.Entity;
using System.Collections.Generic;

namespace Repository
{
    public class MicroServiceContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        public MicroServiceContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
