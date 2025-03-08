using Microsoft.EntityFrameworkCore;
using ProductService.Helper;
using ProductService.Model.Entity;

namespace ProductService.Repository
{
    public class ProductServiceContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Product> Products { get; set; }
        public ProductServiceContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DEFAULT_CONNECTION"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }

    }
}
