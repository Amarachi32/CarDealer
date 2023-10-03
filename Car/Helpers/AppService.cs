using Car.DLL.Entities;

namespace Car.Helpers
{
    public static class AppService
    {
        public static void ConfigureUpload(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySetting"));

        }
    }
}
