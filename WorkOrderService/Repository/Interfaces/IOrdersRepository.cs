using OrderService.Model.Entity;

namespace WorkOrderService.Repository.Interfaces
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllOrders();
        Task InsertOrder(Order order);
    }
}
