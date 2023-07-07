using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationservicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration){
            

        // Adding our DbContext "StoreContext" as a service, to the startup when launching our application

        services.AddDbContext<StoreContext>(opt => 
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddSingleton<IConnectionMultiplexer>(_ => {
            var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddScoped<InterfaceTokenService, TokenService>();
        services.AddScoped<InterfaceBasketRepository, BasketRepository>();
        services.AddScoped(typeof(InterfaceRepository<>), typeof(Repository<>));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<ApiBehaviorOptions>(opt => {
            opt.InvalidModelStateResponseFactory = ActionContext => {
                var errors = ActionContext.ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse{
                    Errors = errors
                };

                return (new BadRequestObjectResult(errorResponse));
            };
        });

        /* If we want to use HTTPS, switch from http -> https, and fix certificate */
        services.AddCors(options => {
            options.AddPolicy("CorsPolicy", policy => {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            });
        });
        
        return services;
        }
    }
}