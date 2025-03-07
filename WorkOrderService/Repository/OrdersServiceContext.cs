using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Model.Entity;

namespace WorkOrderService.Repository
{
    public class OrdersServiceContext : DbContext
    {
        public OrdersServiceContext(DbContextOptions<OrdersServiceContext> options)
        : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
    }
}
