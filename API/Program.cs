// Creates an web application instance
using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIndentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

// Build the web appliction instance
var app = builder.Build();

// Configure the HTTP request pipeline.
// MIDDLEWARE 
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwaggerDocumentation();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Create database and try to migrate the migration 
using var scoped = app.Services.CreateScope();
var service = scoped.ServiceProvider;
var context = service.GetRequiredService<StoreContext>();
var identityContext = service.GetRequiredService<AppIdentityDbContext>();
var userManager = service.GetRequiredService<UserManager<AppUser>>();
var logger = service.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);

    await identityContext.Database.MigrateAsync();
    await AppIdentityContextSeed.SeedAsync(userManager);
}
catch (Exception exception)
{
    logger.LogError(exception, "Migration failed");
}

app.Run();
