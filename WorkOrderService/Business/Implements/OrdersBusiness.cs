using OrderService.Model.Entity;
using OrderService.Model.Request;
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

        public async Task<List<Order>> GetAllOrders()
        {
            return await _ordersRepository.GetAllOrders();
        }

        public async Task CreateOrder(CreateOrderRequest createOrderRequest)
        {
            await _ordersRepository.InsertOrder(new()
            {
                Message = createOrderRequest.Message
            });
        }
    }
}
