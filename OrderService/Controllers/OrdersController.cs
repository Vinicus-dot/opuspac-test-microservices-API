using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Model;
using OrderService.Business.Interfaces;
using Model.Order.DTO;

namespace OrderService.Controllers
{
    public class OrdersController : GenericController
    {
        private readonly IOrdersBusiness _ordersBusiness;
        public OrdersController(IOrdersBusiness ordersBusiness)
        {
            _ordersBusiness = ordersBusiness;
        }

        /// <summary>
        /// List all processed service orders
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves a list of all processed orders, including relevant details such as order ID, customer information, and status.
        /// </remarks>
        /// <response code="200">Orders successfully retrieved</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [SwaggerResponse(200, "Orders successfully retrieved.", typeof(ListResponse<OrderDTO>))]
        [SwaggerResponse(400, "Bad request.")]
        [SwaggerResponse(500, "Internal server error.")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _ordersBusiness.GetAllOrders(pageNumber, pageSize));
        }
    }
}
