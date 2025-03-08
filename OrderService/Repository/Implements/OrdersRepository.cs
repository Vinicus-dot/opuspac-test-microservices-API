using Microsoft.EntityFrameworkCore;
using OrderService.Model.Entity;
using OrderService.Repository.Interfaces;

namespace OrderService.Repository.Implements
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OrdersServiceContext _context;
        public OrdersRepository(OrdersServiceContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task InsertOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
