using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.DTO.RequestDtos
{
    public class CartDto
    {
        public string Id {  get; set; }
        public List<ShoppingCartItemDto> Items { get; set; }
        public int? DeliveryId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
