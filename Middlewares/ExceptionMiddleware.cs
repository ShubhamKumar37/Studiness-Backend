using Backend.Exceptions;
using Backend.Shared.Responses;

namespace Backend.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _iLogger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> iLogger)
        {
            this._next = next;
            this._iLogger = iLogger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ApiException ex)
            {
                _iLogger.LogWarning(ex, "Application Exception : {Message}\n Stack Trace : {StackTrace}", ex.Message, ex.StackTrace);
                await HandleException(context, ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _iLogger.LogWarning(ex, "Application Exception: {Message}\n Stack Trace : {StackTrace}", ex.Message, ex.StackTrace);
                await HandleException(context, ex, 500);
            }
        }

        public static Task HandleException(HttpContext context, Exception ex, int StatusCode)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = StatusCode;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
