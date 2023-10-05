using AutoMapper;
using Car.BLLayer.Interfaces;
using Car.DLL.Entities;
using Car.DLL.Interfaces;
using Microsoft.Extensions.Configuration;
using PayStack.Net;

namespace Car.BLLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<ShoppingCart> _cartRepository;
        private readonly IRepository<DeliveryMethod> _deliveryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public PaymentService(IRepository<ShoppingCart> cartRepository, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, IRepository<DeliveryMethod> deliveryRepository, IRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _deliveryRepository = deliveryRepository;
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            try
            {
                // Get the PayStack secret key from the configuration.
                var testOrLiveSecret = _config["PayStackSetting:SecretKey"];

                // Create a new PayStackApi object.
                var api = new PayStackApi(testOrLiveSecret);

                // Get the cart object from the repository.
                var cart = await _cartRepository.GetByIdAsync(cartId);
                var shippingPrice = 0m;

                if (cart.DeliveryId.HasValue)
                {
                    var deliveryMethod = await _deliveryRepository.GetByIdAsync(cart.DeliveryId);
                    shippingPrice = deliveryMethod.Price;
                }

                foreach (var item in cart.Items)
                {
                    var productItem = await _productRepository.GetByIdAsync(item.Id);

                    if (item.Price != productItem.Price)
                    {
                        item.Price = productItem.Price;
                    }
                }

                // Generate a unique reference for this transaction.
                var reference = Guid.NewGuid().ToString();

                // Check if the cart already has a payment intent ID.
                if (string.IsNullOrEmpty(cart.PaymentIntentId))
                {
                    // Calculate the total amount in kobo (100 kobo = 1 NGN).
                    //var amountInKobo = (long)(cart.Items.Sum(i => i.Quantity * (i.Price * 100)) + (cart.ShippingPrice * 100));
                    var amountInKobo = (long)(cart.Items.Sum(i => i.Quantity * (i.Price * 100)) + (shippingPrice * 100));



                    // Create the payment intent.
                    var result = api.Post<ApiResponse<dynamic>, dynamic>("/transaction/initialize", new
                    {
                        amount = amountInKobo,
                        currency = "NGN",
                        reference = reference,
                        PaymentMethodTypes = new List<string> { "card" }
                    });

                    if (result.Status)
                    {
                        // Payment intent created successfully.
                        // Save the payment intent ID to the cart object.
                        cart.PaymentIntentId = result.Data.data.reference;

                        // Update the cart object in the repository.
                        await _cartRepository.UpdateAsync(cart);

                        // Return a success response.
                        return new ApiResponse<ShoppingCart>
                        {
                            Status = true,
                            Data = cart
                        };
                    }
                    else
                    {
                        return new ApiResponse<ShoppingCart> { Status = false, Message = "payment intent creation failed" };

                    }
                }
                else
                {
                    // Payment intent already exists, return a success response indicating that.
                    // return new ApiResponse<ShoppingCart> { Status = true, Data = new { message = "Payment intent already exists." } };
                    return new ApiResponse<ShoppingCart> { Status = true, Message = "Payment intent already exists." };
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here.
                return new ApiResponse<ShoppingCart> { Status = false, Message = ex.Message };
            }
        }


    }
}
//        var response = await paystackTransactionAPI.InitializeTransaction(model.email, model.amount, model.firstName, model.lastName, "http://localhost:17869/order/callback");
