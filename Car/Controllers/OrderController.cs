using AutoMapper;
using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
using Car.BLLayer.Extension;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        public OrderController(IOrderService service, IMapper mapper)
        {
            _orderService = service;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetieveEmailFromPrincipal();
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.OrderId, address);
            if (order != null)
            {
                return BadRequest(_response);
            }
            return Ok(order);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReturnDto>> GetOrderById(OrderDto orderDto)
        {
            var email = HttpContext.User.RetieveEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(orderDto.OrderId, email);
            if (order != null)
            {
                return BadRequest(_response);
            }
            return Ok(_mapper.Map<IEnumerable<OrderReturnDto>>(order));
        }

        [HttpGet]
        public async Task<ActionResult<OrderReturnDto>> GetOrderByUser()
        {
            var email = HttpContext.User.RetieveEmailFromPrincipal();
            var order = await _orderService.GetOrdersForUserAsync(email);
            if (order != null)
            {
                return BadRequest(_response);
            }
            return Ok(_mapper.Map<IEnumerable<OrderReturnDto>>(order));
        }

        [HttpGet("delivery")]
        public async Task<ActionResult<IEnumerable<OrderReturnDto>>> GetDeliveryMethod()
        {
            var order = await _orderService.GetDeliveryMethodAsync();
            if (order != null)
            {
                return BadRequest(_response);
            }
            return Ok(_mapper.Map<IEnumerable<OrderReturnDto>>(order));
        }
    }
}
