using Car.BLLayer.DTO.ResponseDto;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        //private readonly IMapper _mapper;
        protected ResponseDto _response;

        public PaymentController(IPaymentService paymentService, ResponseDto response)
        {
            _paymentService = paymentService;
            _response = response;
        }

        [HttpPost]
        public async Task<ApiResponse<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            return await _paymentService.CreateOrUpdatePaymentIntent(cartId);
        }
    }
}
