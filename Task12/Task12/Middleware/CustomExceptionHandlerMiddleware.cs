using System.Net;
using System.Text.Json;

namespace Task12.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public CustomExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch(Exception ex)
            {
                await HandlerExceptionAsync(context, ex);
            }
        }

        private Task HandlerExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (ex)
            {
                case InvalidOperationException invalidOperationException:
                    code = HttpStatusCode.NotFound;
                    break;

            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if(result == String.Empty)
            {
                result = JsonSerializer.Serialize(new { error = ex.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
