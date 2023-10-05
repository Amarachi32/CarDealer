
using AutoMapper;
using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
        private readonly IPhotoServices _photoServices;
        private readonly IMapper _mapper;
        protected ResponseDto _response;
        public ProductController(IProductServices service, IPhotoServices photoServices, IMapper mapper)
        {
            _productService = service;
            _photoServices = photoServices;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResponseDto>>> Get([FromQuery] ProductParams param)
        {
            try
            {
                Pagination<ProductResponseDto> productDtos = await _productService.GetProducts(param);
                return productDtos;
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ApiResponseType), StatusCodes.Status404NotFound)]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await _productService.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]

        public async Task<object> Post([FromForm] ProductDto productDto)
        {
            try
            {
                var imageUrl = await _photoServices.AddPhotoAsync(productDto.ImageFile);
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    PictureUrl = imageUrl,
                    ProductBrandId = productDto.ProductBrand,
                    ProductTypeId = productDto.ProductType
                };

                //var product = _mapper.Map<ProductDto, Product>(productDto);
                // Set the PictureUrl property to the Cloudinary URL
                //product.PictureUrl = imageUrl;

                ProductDto model = await _productService.CreateUpdateProduct(product);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productService.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productService.DeleteProduct(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("brand")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetProductBrand()
        {
            var productBrands = await _productService.GetProductBrandsAsync();
            var brandDtos = productBrands.Select(brand => new BrandDto
            {
                Name = brand.Name
            }).ToList();

            return Ok(brandDtos);
        }

        [HttpGet("type")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetProductType()
        {
            var productTypes = await _productService.GetProductTypesAsync();
            var typeDtos = productTypes.Select(type => new TypeDto
            {
                Name = type.Name
            }).ToList();

            return Ok(typeDtos);
        }
    }
}
