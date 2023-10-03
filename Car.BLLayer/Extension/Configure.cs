using Car.BLLayer.Interfaces;
using Car.BLLayer.Services;
using Car.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Extension
{
    public static class Configuret
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CarDbContext>(OptionsBuilder => {
                OptionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
        }

        public static void MemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<ICartService, CartService>();
            return services;

        }
    }
}
