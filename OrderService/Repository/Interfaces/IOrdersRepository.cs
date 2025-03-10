using Model;
using OrderService.Model.Entity;

namespace OrderService.Repository.Interfaces
{
    public interface IOrdersRepository
    {
        Task<ListResponse<Order>> GetAllOrders(int pageNumber, int pageSize);
        Task InsertOrder(Order order);
    }
}
