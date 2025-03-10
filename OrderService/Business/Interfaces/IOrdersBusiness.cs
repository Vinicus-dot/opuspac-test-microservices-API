using Model;
using OrderService.Model.DTO;
using OrderService.Model.Request;


namespace OrderService.Business.Interfaces
{
    public interface IOrdersBusiness
    {
        Task<ListResponse<OrderDTO>> GetAllOrders(int pageNumber, int pageSize);
        Task CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
