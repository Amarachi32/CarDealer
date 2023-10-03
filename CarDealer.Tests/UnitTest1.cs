using AutoMapper;
using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.Services;
using Car.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Tests
{
    public class Tests
    {
        private ProductService _productService;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            _productService = new ProductService(_unitOfWork, _mapper);
        }

        [Test]
        public async Task CreateNewProduct_ShouldCreateProduct()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Name = "Test Product",
                Description = "This is a test product.",
                Price = 100.00m
            };

            // Act
            var result = await _productService.CreateUpdateProduct(productDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productDto.Name, result.Name);
            Assert.AreEqual(productDto.Description, result.Description);
            Assert.AreEqual(productDto.Price, result.Price);
        }
    }
}