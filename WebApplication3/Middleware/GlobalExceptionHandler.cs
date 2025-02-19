using System.Net;
using System.Text.Json;

namespace WebApplication3.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate request, ILogger<GlobalExceptionHandler> logger)
        {
            _request = request;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var start = DateTime.Now;
            _logger.LogInformation("Handling Request..");
            await _request(context);
            var end = DateTime.Now;
            var duration = end - start;
            _logger.LogInformation($" Finishing handling Request in {duration.Milliseconds}ms..");

        }

      
        
    }
}
