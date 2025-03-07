using Microsoft.EntityFrameworkCore;
using OrderService.Model.Entity;
using WorkOrderService.Repository.Interfaces;

namespace WorkOrderService.Repository.Implements
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
    }
}
