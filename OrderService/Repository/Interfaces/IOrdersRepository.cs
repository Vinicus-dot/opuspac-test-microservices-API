using OrderService.Model.Entity;

namespace OrderService.Repository.Interfaces
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllOrders();
        Task InsertOrder(Order order);
    }
}
