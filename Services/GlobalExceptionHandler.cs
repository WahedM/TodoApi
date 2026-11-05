using Microsoft.AspNetCore.Diagnostics;

namespace TodoApi.Services
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct  )
        {
            logger.LogError(exception, "An unhandled exception occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var errorResponse = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Details = exception.Message
            };
            await context.Response.WriteAsJsonAsync(errorResponse, ct);
            return true;
        }

    }
}
