using OrderService.Model.DTO;
using OrderService.Model.Entity;
using OrderService.Model.Request;
using OrderService.Model.Response;

namespace OrderService.Business.Interfaces
{
    public interface IOrdersBusiness
    {
        Task<ListResponse<OrderDTO>> GetAllOrders(int pageNumber, int pageSize);
        Task CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
