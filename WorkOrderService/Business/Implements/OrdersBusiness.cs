using OrderService.Model.Entity;
using WorkOrderService.Business.Interfaces;
using WorkOrderService.Repository.Interfaces;

namespace WorkOrderService.Business.Implements
{
    public class OrdersBusiness : IOrdersBusiness
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersBusiness(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Task<List<Order>> GetAllOrders()
        {
            return _ordersRepository.GetAllOrders();
        }
    }
}
