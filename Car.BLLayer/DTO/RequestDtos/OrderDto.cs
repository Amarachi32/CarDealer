using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.DTO.RequestDtos
{
    public class OrderDto
    {
        public string OrderId { get; set; }
        public string DeliveryMethodId { get; set;}
        public AddressDto ShipToAddress { get; set; }
    }
}
