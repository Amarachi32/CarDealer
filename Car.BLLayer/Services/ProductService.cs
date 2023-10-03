using AutoMapper;
using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Car.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Services
{
    public class ProductService :IProductServices
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = _unitOfWork.GetRepository<Product>();
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            if (product.Id.Any())
            {
                await _productRepository.UpdateAsync(product);
            }
            else
            {
                await _productRepository.AddAsync(product);
            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            try
            {
                Product product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return false;
                }
                await _productRepository.DeleteAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(string productId)
        {
            var product = await _productRepository.GetSingleByAsync(
             p => p.Id == productId,
             include: q => q.Include(p => p.ProductBrand).Include(p => p.ProductType));

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<Product> productList = await _productRepository
                .GetAllAsync(include: q => q.Include(p => p.ProductBrand).Include(p => p.ProductType));
            return _mapper.Map<List<ProductDto>>(productList);
        }

/*        public async Task<IEnumerable<ProductBrand>> GetProductBrandsAsync()
        {
            IEnumerable<ProductBrand> productBrand = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductBrand>>(productBrand);
        }

        public Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            IEnumerable<ProductType> productType = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductType>>(productType);
        }*/
    }
}
