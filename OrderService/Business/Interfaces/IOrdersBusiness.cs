using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.Model.Request;

namespace OrderService.Business.Interfaces
{
    public interface IOrdersBusiness
    {
        Task<List<OrderDTO>> GetAllOrders();
        Task CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
