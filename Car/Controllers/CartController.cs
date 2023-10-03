using Car.BLLayer.DTO.ResponseDto;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        protected ResponseDto _response;
        public CartController(ICartService service)
        {
            _cartService = service;
            _response = new ResponseDto();
        }



        [HttpPost("Cart/items")]
        public async Task<ActionResult> AddItemToShoppingCartAsync([FromBody] ShoppingCartItem item)
        {
            var userId = User.Identity.Name;

            var shoppingCart = await _cartService.GetShoppingCartAsync(userId);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
            }

            shoppingCart.Items.Add(item);

            await _cartService.SetShoppingCartAsync(userId, shoppingCart);

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetShoppingCartAsync()
        {
            var userId = User.Identity.Name;
            var shoppingCart = await _cartService.GetCartAsync(userId);

            return Ok(shoppingCart);
        }
        [HttpPost("add item")]
        public async Task<IActionResult> AddItemToCartAsync([FromBody] string itemId)
        {
            var userId = User.Identity.Name;
            await _cartService.AddItemToCartAsync(userId, itemId);

            return Ok();
        }
        [HttpDelete("remove item")]
        public async Task<IActionResult> RemoveItemFromCartAsync([FromBody] string itemId)
        {
            var userId = User.Identity.Name;
            await _cartService.RemoveItemFromCartAsync(userId, itemId);

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> ClearCartAsync()
        {
            var userId = User.Identity.Name;
            await _cartService.ClearCartAsync(userId);

            return Ok();
        }
    }
}
