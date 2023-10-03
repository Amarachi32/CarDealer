using Car.DLL.Entities;
using Car.DLL.Enum;
using Microsoft.AspNetCore.Http;

namespace Car.BLLayer.DTO.ResponseDto
{
    public class ProductResponseDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }

    public class OrderReturnDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }

        public decimal Total { get; set; }
    }

    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
