using Car.BLLayer.Data.SeedData;
using Car.BLLayer.Extension;
using Car.DLL.Models;
using Car.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Logging.AddConsole();
        builder.Services.AddControllers();
       // builder.Services.AddControllers().AddNewtonsoftJson();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureServices(builder.Configuration);
        builder.Services.ConfigureUpload(builder.Configuration);
        builder.Services.ApplicationServices();
        builder.Services.ConfigureJWT(builder.Configuration);
        builder.Services.RegisterService(builder.Configuration);
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddAuthorization();
        //builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // builder.Services.AddAutoMapper(typeof(MappingProfile));
        ///builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Standard Authorization Header Using the Bearer Scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.EnableAnnotations();
            options.UseInlineDefinitionsForEnums();
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<CarDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
             await context.Database.MigrateAsync();
            await CarContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "an error occur during migration");

        }

        await app.RunAsync();
    }
}