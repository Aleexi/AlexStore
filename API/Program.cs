// Creates an web application instance
using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIndentityServices(builder.Configuration);

// Build the web appliction instance
var app = builder.Build();

// Configure the HTTP request pipeline.
// MIDDLEWARE 
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// Create database and try to migrate the migration 
using var scoped = app.Services.CreateScope();
var service = scoped.ServiceProvider;
var context = service.GetRequiredService<StoreContext>();
var logger = service.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception exception)
{
    logger.LogError(exception, "Migration failed");
}

app.Run();
