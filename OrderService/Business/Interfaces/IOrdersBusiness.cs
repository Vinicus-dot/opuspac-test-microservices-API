using OrderService.Model.Entity;
using OrderService.Model.Request;

namespace OrderService.Business.Interfaces
{
    public interface IOrdersBusiness
    {
        Task<List<Order>> GetAllOrders();
        Task CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
