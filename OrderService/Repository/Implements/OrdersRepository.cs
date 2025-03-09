using Microsoft.EntityFrameworkCore;
using OrderService.Model.Entity;
using OrderService.Model.Response;
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

        public async Task<ListResponse<Order>> GetAllOrders(int pageNumber, int pageSize)
        {
            var orders = await _context.Orders.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new ListResponse<Order>
            {
                Data = orders,
                Total = await _context.Orders.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task InsertOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
