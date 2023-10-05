using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
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
        Task<Pagination<ProductResponseDto>> GetProducts(ProductParams param);
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(Product product);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<ProductBrand>> GetProductBrandsAsync();
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
    }
}
