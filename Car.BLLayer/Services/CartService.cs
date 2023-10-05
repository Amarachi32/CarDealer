using AutoMapper;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Car.DLL.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Services
{
    public class CartService : ICartService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<ShoppingCart> _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartService(IMemoryCache memoryCache, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartRepository = _unitOfWork.GetRepository<ShoppingCart>();
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string userId)
        {
            var shoppingCart = _memoryCache.Get<ShoppingCart>(userId);
            if (shoppingCart == null)
            {
                return null;
            }

            return shoppingCart;
        }

        public async Task SetShoppingCartAsync(string userId, ShoppingCart shoppingCart)
        {
            _memoryCache.Set(userId, shoppingCart);
        }

        public async Task<bool> ClearCartAsync(string cartId)
        {
            var cartUserFromDb = await _cartRepository.GetByIdAsync(cartId);
            if (cartUserFromDb != null)
            {
               await _cartRepository.DeleteRangeAsync(x => x.Id == cartId);
               return true;
            }
            return false;
        }

        //get cart by userId
        public async Task<ShoppingCart> GetCartAsync(string userId)
        {
            var cart = _memoryCache.Get<ShoppingCart>(userId);
            if (cart == null)
            {
                cart = await _cartRepository.GetByIdAsync(userId);
                if (cart == null)
                {
                    cart = new ShoppingCart(userId);
                }

                _memoryCache.Set(userId, cart);
            }

            return cart;
        }

        public async Task AddItemToCartAsync(string userId, int itemId)
        {
            var cart = await GetCartAsync(userId);
            cart.Items.Add(new ShoppingCartItem { Id = itemId });

            _memoryCache.Set(userId, cart);

            await SyncCartAsync(userId);
        }

        public async Task RemoveItemFromCartAsync(string userId, int itemId)
        {
            var cart = await GetCartAsync(userId);
           // cart.Items.Remove(cart.Items.FirstOrDefault(x => x.Id == itemId));
            var cartItemToRemove = cart.Items.FirstOrDefault(x => x.Id == itemId);
            if (cartItemToRemove != null && cartItemToRemove.Quantity <= 1)
            {
                // Remove the item from the cart.
                cart.Items.Remove(cartItemToRemove);

                _memoryCache.Set(userId, cart);

                await SyncCartAsync(userId);
            }
        }

        private async Task SyncCartAsync(string userId)
        {
            var cart = await GetCartAsync(userId);
            await _cartRepository.UpdateAsync(cart);
        }

    }
}
