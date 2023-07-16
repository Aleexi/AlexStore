using API.DTO;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Interfaces;
using Core.OrderAggregation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : SuperController
    {
        private readonly InterfaceOrderService orderService;
        private readonly IMapper mapper;
        public OrdersController(InterfaceOrderService orderService, IMapper mapper)
        {
            this.mapper = mapper;
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);

            var order = await this.orderService.CreateOrder(userEmail, orderDTO.DelieveryMethodId, orderDTO.BasketId, address);

            if (order == null) {
                return BadRequest(new ApiResponse(400, "Couldn't create order"));
            }
            
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersByUser()
        {
            var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await this.orderService.GetOrdersByUser(userEmail);

            return Ok(this.mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(int id)
        {
            var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await this.orderService.GetOrderById(id, userEmail);

            if (order == null) {
                return NotFound(new ApiResponse(400));
            }

            return Ok(this.mapper.Map<OrderToReturnDTO>(order));
        }

        [HttpGet("delieveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DelieveryMethod>>> GetDelieveryMethods()
        {
            return Ok(await this.orderService.GetDelieveryMethods());
        }
    }
}