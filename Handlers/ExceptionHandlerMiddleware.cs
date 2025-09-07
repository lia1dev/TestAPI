using System.Net;
using System.Text.Json;

namespace TestAPI.Handlers
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SecureException ex)
            {
                _logger.LogError(ex, "SecureException caught with EventId {EventId}", ex.EventId);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    type = ex.GetType().Name,
                    id = ex.EventId.ToString(),
                    data = new
                    {
                        message = ex.Message
                    }
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                var eventId = Guid.NewGuid();

                _logger.LogError(ex, "Exception caught with EventId {EventId}", eventId);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    type = ex.GetType().Name,
                    id = eventId.ToString(),
                    data = new
                    {
                        message = $"Internal server error ID = {eventId}"
                    }
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}