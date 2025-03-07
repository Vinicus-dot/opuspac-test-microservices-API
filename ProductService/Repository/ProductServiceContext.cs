using Microsoft.EntityFrameworkCore;
using ProductService.Model;

namespace ProductService.Repository
{
    public class ProductServiceContext : DbContext
    {
        public ProductServiceContext(DbContextOptions<ProductServiceContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }

    }
}
