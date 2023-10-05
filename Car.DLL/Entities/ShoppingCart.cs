using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Car.DLL.Entities
{
    public class ShoppingCart //: BaseEntity
    {
        public ShoppingCart(string? id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public int? DeliveryId { get; set; } 
        public string ClientSecret {  get; set; }
        public string PaymentIntentId {  get; set; }
    }
}
