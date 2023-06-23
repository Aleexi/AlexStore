using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment environment)
        {
            this.environment = environment;
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, exception.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                // If we are in development, send ApiException back with details as stack trace, otherwise just send internal server error back.
                var response = this.environment.IsDevelopment() ? new ApiException(500, exception.Message, exception.StackTrace.ToString()) : new ApiException(500);

                var opt = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, opt);

                await context.Response.WriteAsync(json);
            }
        }
    }
}