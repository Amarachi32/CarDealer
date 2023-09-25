using Car.BLLayer.DTO.ResponseDto;
using Car.BLLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        protected ResponseDto _response;
        public OrderController(IOrderService service)
        {
            _orderService = service;
            _response = new ResponseDto();
        }
    }
}
