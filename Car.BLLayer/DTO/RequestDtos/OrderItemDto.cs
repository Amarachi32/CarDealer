using Car.DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.DTO.RequestDtos
{
    public class OrderItemDto
    {

        public decimal ProductId { get; set; }
        public decimal ProductName { get; set; }
        public decimal PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
