using AutoMapper;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Car.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Car.BLLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<DeliveryMethod> _deliveryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ShoppingCart> _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IMemoryCache memoryCache, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderRepository = _unitOfWork.GetRepository<Order>();
            _deliveryRepository = _unitOfWork.GetRepository<DeliveryMethod>();
            _cartRepository = _unitOfWork.GetRepository<ShoppingCart>();
            _productRepository = _unitOfWork.GetRepository<Product>();
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string deliveryMethodId, string cartId, Address shipAddress)
        {
            //get Cart 
            var basket = await _cartRepository.GetByIdAsync(cartId);
            //  get  items from product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _productRepository.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            //get delivery method from the repo
            var deliveryMethod = await _deliveryRepository.GetByIdAsync(deliveryMethodId);
            //calc subtotal
            var subTotal = items.Sum(p => p.Price * p.Quantity);
            //create order
            var order = new Order(items, buyerEmail, shipAddress, deliveryMethod, subTotal);
            //ToDo: save to db
            return order;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _deliveryRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(string Id, string buyerEmail)
        {
            var order = await _orderRepository.GetSingleByAsync(
                o => o.Id == Id && o.BuyerEmail == buyerEmail, // Added Id condition
                include: o => o.Include(o => o.OrderItems).Include(o => o.DeliveryMethod));
            return order;
        }


        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var order = await _orderRepository.GetByAsync(
             o => o.BuyerEmail == buyerEmail,
             include: o => o.Include(o => o.OrderItems).Include(o => o.DeliveryMethod));//.OrderByDescending(o =>o.OrderDate));
            order = order.OrderByDescending(o => o.OrderDate).ToList();
            return order.ToList();
        }
    }
}
