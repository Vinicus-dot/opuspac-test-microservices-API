using Model;
using Model.Order.Entity;

namespace Repository.Interfaces
{
    public interface IOrdersRepository
    {
        Task<ListResponse<Order>> GetAllOrders(int pageNumber, int pageSize);
        Task InsertOrder(Order order);
    }
}
