using AutoMapper;
using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Car.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.BLLayer.Services
{
    public class ProductService : IProductServices
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductBrand> _brandRepository;
        private readonly IRepository<ProductType> _typeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
            _productRepository = _unitOfWork.GetRepository<Product>();
            _brandRepository = _unitOfWork.GetRepository<ProductBrand>();
            _typeRepository = _unitOfWork.GetRepository<ProductType>();
        }

        public async Task<ProductDto> CreateUpdateProduct(Product product)
        {
            //Product product = _mapper.Map<ProductDto, Product>(productDto);
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);

            if (existingProduct != null)
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

        public Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProduct(int productId)
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

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _productRepository.GetSingleByAsync(
             p => p.Id == productId,
             include: q => q.Include(p => p.ProductBrand).Include(p => p.ProductType));

            return _mapper.Map<ProductDto>(product);
        }

        //public async Task<IEnumerable<ProductDto>> GetProducts([FromQuery] int? skip, [FromQuery] int? take, [FromQuery] int? brandId, [FromQuery] int? typeId, string orderByCriteria = null)
        public async Task<Pagination<ProductResponseDto>> GetProducts(ProductParams param)
        {
            var totalCount = await _productRepository.CountAsync(predicate: x =>
            (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search.ToLower())) &&
            (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) &&
            (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId));

            IEnumerable<Product> productList = await _productRepository
                .GetByAsync(predicate: x =>
                (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search.ToLower())) &&
                (!param.BrandId.HasValue || x.ProductBrandId == param.BrandId) &&
                (!param.TypeId.HasValue || x.ProductTypeId == param.TypeId),
            orderBy: GetOrderByExpression(param.sort),
            //skip: param.PageIndex,
            //skip: (param.PageIndex - 1) * param.PageSize,
            skip: param.PageIndex >= 1 ? (param.PageIndex - 1) * param.PageSize : (int?)null, // Correctly calculate skip
            //skip: param.PageIndex.HasValue ? (param.PageIndex.Value - 1) * param.PageSize : (int?)null, // Correctly calculate skip
            take: param.PageSize,
            include: q => q.Include(p => p.ProductBrand).Include(p => p.ProductType));
            var productDtos = _mapper.Map<IEnumerable<ProductResponseDto>>(productList);

            return new Pagination<ProductResponseDto>(param.PageIndex, param.PageSize, totalCount, productDtos);

        }

        private Func<IQueryable<Product>, IOrderedQueryable<Product>> GetOrderByExpression(string orderByCriteria)
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderByExpression;

            if (!string.IsNullOrEmpty(orderByCriteria))
            {
                switch (orderByCriteria.ToLower())
                {
                    case "priceasc":
                        orderByExpression = query => query.OrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        orderByExpression = query => query.OrderByDescending(p => p.Price);
                        break;
                    default:
                        orderByExpression = query => query.OrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                // Default sorting by Name if no orderByCriteria is provided
                orderByExpression = query => query.OrderBy(p => p.Name);
            }

            return orderByExpression;
        }
        
        public async Task<IEnumerable<ProductBrand>> GetProductBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            return brands;
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            var types = await _typeRepository.GetAllAsync();
            return types;
        }
    }
}
