using Car.BLLayer.DTO.RequestDtos;
using Car.DLL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Interfaces
{
    public interface IProductServices
    {
        Task<Pagination<ProductDto>> GetProducts(ProductParams param);
        Task<ProductDto> GetProductById(string productId);
        Task<ProductDto> CreateUpdateProduct(Product product);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(string productId);
/*        Task<IEnumerable<ProductBrand>> GetProductBrandsAsync();
        Task<IEnumerable<ProductType>> GetProductTypesAsync();*/
    }
}
