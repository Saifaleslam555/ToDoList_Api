using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Error;

namespace ToDoList.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate _next
            ,ILogger<ExceptionMiddleware> _logger
            ,IHostEnvironment _env)
        {
            this._next = _next;
            this._logger = _logger;
            this._env = _env;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) {

                _logger.LogError(e,e.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, e.Message, e.StackTrace)
                    : new ApiException(context.Response.StatusCode, e.Message, "interal server error");

                var optian = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response,optian);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
