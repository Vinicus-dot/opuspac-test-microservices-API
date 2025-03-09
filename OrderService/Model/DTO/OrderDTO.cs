using OrderService.Model.Entity;

namespace OrderService.Model.DTO
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public string Message { get; set; }

        public OrderDTO() { }

        public OrderDTO(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            Id = order.Id;
            Message = order.Message;
        }

        public List<OrderDTO> ToListDto(IEnumerable<Order> orders)
        {
            ArgumentNullException.ThrowIfNull(orders);
            return orders.Select(o => new OrderDTO(o)).ToList();
        }
    }
}
