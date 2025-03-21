﻿using OrderService.Model.Entity;
using OrderService.Model.Request;
using OrderService.Business.Interfaces;
using OrderService.Repository.Interfaces;
using OrderService.Model.DTO;
using ServiceStack.Host;
using Model;

namespace OrderService.Business.Implements
{
    public class OrdersBusiness : IOrdersBusiness
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersBusiness(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<ListResponse<OrderDTO>> GetAllOrders(int pageNumber, int pageSize)
        {
            var orders =  await _ordersRepository.GetAllOrders(pageNumber, pageSize);
            return new ListResponse<OrderDTO>
            {
                Data = new OrderDTO().ToListDto(orders.Data),
                Total = orders.Total,
                PageNumber = orders.PageNumber,
                PageSize = orders.PageSize
            };
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
