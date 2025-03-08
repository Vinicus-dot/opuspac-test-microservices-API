using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OrderService.Model.Entity;

namespace OrderService.Repository
{
    public class OrdersServiceContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Order> Orders { get; set; }

        public OrdersServiceContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DEFAULT_CONNECTION"));
        }
    }
}
