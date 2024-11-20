using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using TalabatG02.APIs.Dtos;
using TalabatG02.APIs.Errors;
using TalabatG02.Core.Entities.OrderAggregtion;
using TalabatG02.Core.Services;
using Order = TalabatG02.Core.Entities.OrderAggregtion.Order;

namespace TalabatG02.APIs.Controllers
{
    [Authorize]
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<ActionResult<Order>> CreatOrder(OrderDto OrderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = mapper.Map<AddressDto, Address>(OrderDto.ShippingAddress);

            var Order = await orderService.CreateOrderAsync(buyerEmail, OrderDto.BasketId, OrderDto.DeliveryMethodId, address);

            if (Order is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(Order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await orderService.GetOrderForUserAsync(buyerEmail);

            var mappedOrder = mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(mappedOrder);
        }


        //[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await orderService.GetOrderByIdForUserAsync(id, buyerEmail);

            if(Order is null) return NotFound(new ApiErrorResponse(404));

            var mappeOrder = mapper.Map<Order, OrderToReturnDto>(Order);

            return Ok(mappeOrder);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods =await orderService.GetDeliveryMethodsAsync();
            return Ok (deliveryMethods);
        }



    }
}
