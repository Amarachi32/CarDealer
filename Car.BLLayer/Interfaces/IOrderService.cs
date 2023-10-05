using Car.DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string deliveryMethodId, int cartId, Address shipAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int Id, string buyerEmail);
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
