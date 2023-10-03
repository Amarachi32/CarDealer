using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.DTO.RequestDtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; }
        public int ProductType { get; set; }
        public int ProductBrand { get; set; }
    }
}
