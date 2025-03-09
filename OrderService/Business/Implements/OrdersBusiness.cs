using OrderService.Model.Entity;
using OrderService.Model.Request;
using OrderService.Business.Interfaces;
using OrderService.Repository.Interfaces;
using OrderService.Model.DTO;
using ServiceStack.Host;

namespace OrderService.Business.Implements
{
    public class OrdersBusiness : IOrdersBusiness
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersBusiness(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<OrderDTO>> GetAllOrders()
        {
            var orders =  await _ordersRepository.GetAllOrders();
            return new OrderDTO().ToListDto(orders);
        }

        public async Task CreateOrder(CreateOrderRequest createOrderRequest)
        {
            if (string.IsNullOrEmpty(createOrderRequest.Message))
                throw new HttpException(StatusCodes.Status400BadRequest, "Message deve ser preenchido!");

            await _ordersRepository.InsertOrder(new()
            {
                Message = createOrderRequest.Message
            });
        }
    }
}
