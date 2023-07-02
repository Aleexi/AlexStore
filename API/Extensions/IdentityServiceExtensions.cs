using Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIndentityServices(this IServiceCollection services, IConfiguration config) {
            services.AddDbContext<AppIdentityDbContext>(options => {
                options.UseSqlite(config.GetConnectionString("IdentityConnection"));
            });
            return services;
        }
    }
}