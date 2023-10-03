using Car.DLL.Entities;
using Car.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Car.BLLayer.Data.SeedData
{
    public class CarContextSeed
    {
        public static async Task SeedAsync(CarDbContext context)
        {

            if (!context.ProductBrands.Any())
            {

                try
                {
                    var brandsData = File.ReadAllText("../Car.BLLayer/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    context.ProductBrands.AddRange(brands);
                    // Rest of the code
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, display error, etc.)
                    //"an error occur during migration";

                }
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Car.BLLayer/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Car.BLLayer/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            /*if (!context.Orders.Any())
            {
                var orderData = File.ReadAllText("../Car.BLLayer/Data/SeedData/orders.json");
                var orders = JsonSerializer.Deserialize<List<Order>>(orderData);
                context.Orders.AddRange(orders);
            }*/

            /*          if (!context.DeliveryMethods.Any())
                      {
                          var deliveryData = File.ReadAllText("../Car.BLLayer/Data/SeedData/orders.json");
                          var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                          context.DeliveryMethods.AddRange(delivery);
                      }*/

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
