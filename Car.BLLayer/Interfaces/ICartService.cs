using Car.DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Interfaces
{
    public interface ICartService
    {
        Task<ShoppingCart> GetShoppingCartAsync(string userId);
        Task SetShoppingCartAsync(string userId, ShoppingCart shoppingCart);
        Task<bool> ClearCartAsync(string cartId);
        Task<ShoppingCart> GetCartAsync(string userId);
        Task AddItemToCartAsync(string userId, int itemId);
        Task RemoveItemFromCartAsync(string userId, int itemId);
    }
}
