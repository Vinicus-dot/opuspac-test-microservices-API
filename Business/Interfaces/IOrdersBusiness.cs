using Model;
using Model.Order.DTO;
using Model.Order.Request;

namespace OrderService.Business.Interfaces
{
    public interface IOrdersBusiness
    {
        Task<ListResponse<OrderDTO>> GetAllOrders(int pageNumber, int pageSize);
        Task CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
