using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.Interfaces;
using Car.BLLayer.Services;
using Car.DLL.Entities;
using Car.DLL.Interfaces;
using Car.DLL.Models;
using Car.DLL.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Car.BLLayer.Extension
{
    public static class Configuret
    {
        private static readonly string _policyName = "CorsPolicy";
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CarDbContext>(OptionsBuilder =>
            {
                OptionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

        }

        public static void MemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
         
        }
 
        // static IServiceCollection ApplicationServices(this IServiceCollection services)
        public static void ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();

            services.AddTransient<IUnitOfWork, UnitOfWork<CarDbContext>>();
            services.AddScoped<IMemoryCache, MemoryCache>();
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<IPhotoServices, PhotoService>();
            services.AddScoped<ICartService, CartService>();
            //return services;

        }

        public static void RegisterService(this IServiceCollection services, IConfiguration config)
        {
            //object value = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<CarDbContext>()
            .AddDefaultTokenProviders();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IValidator<UpdateRequestDto>, UpdateRequestValidator>();
            services.AddScoped<ValidationFilterAttribute>();
            services.AddTransient<IJWTAuthenticator, JwtAuthenticator>();
        }
    }
}
