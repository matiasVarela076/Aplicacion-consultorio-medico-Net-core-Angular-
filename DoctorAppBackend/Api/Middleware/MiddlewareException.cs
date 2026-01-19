using System.Net;
using System.Text.Json;
using Api.Errores;

namespace Api.Middleware
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<MiddlewareException> _logger;

        private readonly IHostEnvironment _env;

        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) // status code, mensaje, detalle
                                : new ApiErrorResponse(context.Response.StatusCode);
               var opciones = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
               var json = JsonSerializer.Serialize(response, opciones);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
