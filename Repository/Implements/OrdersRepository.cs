using Microsoft.EntityFrameworkCore;
using Model;
using Model.Order.Entity;
using Repository.Interfaces;

namespace Repository.Implements
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly MicroServiceContext _context;
        public OrdersRepository(MicroServiceContext context)
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
