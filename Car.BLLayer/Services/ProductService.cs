using AutoMapper;
using Car.BLLayer.DTO.RequestDto;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Car.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Services
{
    public class ProductService :IProductServices
    {
        private readonly IRepository<Product> _providerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _providerRepository = _unitOfWork.GetRepository<Product>();
        }

        public Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
