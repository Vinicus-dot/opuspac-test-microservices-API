using OrderService.Model.Entity;
using OrderService.Model.Response;

namespace OrderService.Repository.Interfaces
{
    public interface IOrdersRepository
    {
        Task<ListResponse<Order>> GetAllOrders(int pageNumber, int pageSize);
        Task InsertOrder(Order order);
    }
}
