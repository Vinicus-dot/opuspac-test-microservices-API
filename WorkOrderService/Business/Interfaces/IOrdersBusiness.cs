using OrderService.Model.Entity;

namespace WorkOrderService.Business.Interfaces
{
    public interface IOrdersBusiness
    {
        Task<List<Order>> GetAllOrders();
    }
}
