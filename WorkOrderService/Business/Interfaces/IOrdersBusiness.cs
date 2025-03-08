using OrderService.Model.Entity;
using OrderService.Model.Request;

namespace WorkOrderService.Business.Interfaces
{
    public interface IOrdersBusiness
    {
        Task<List<Order>> GetAllOrders();
        Task CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
