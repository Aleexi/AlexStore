using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(conf => {
                var securitySchema = new OpenApiSecurityScheme {
                    Description = "JWToken authentication bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                conf.AddSecurityDefinition("Bearer", securitySchema);
                
                var securityRequirments = new OpenApiSecurityRequirement{
                    {
                        securitySchema, new []{"Bearer"}
                    }
                };

                conf.AddSecurityRequirement(securityRequirments);
                
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app) {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}