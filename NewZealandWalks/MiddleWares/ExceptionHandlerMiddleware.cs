
using System.Net;

namespace NewZealandWalks.MiddleWares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger1;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger1,RequestDelegate next)
        {
            this.logger1 = logger1;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await next(httpContext);
            }
            catch (Exception ex)
            {
               var errorId = Guid.NewGuid();

               logger1.LogError(ex, $"{errorId}:{ex.Message}");

                httpContext.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType= "application/json";

                var error = new
                {
                    id = errorId,
                    message = "Something went wrong we are on it."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
