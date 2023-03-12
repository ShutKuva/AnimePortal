using System.Globalization;
using System.Text;

namespace AnimePortalAuthServer.Middlewares
{
    //TODO
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(ex.Message));
            }
        }
    }

    public static class ErrorHandlerMiddlewareExtension
    {
        public static WebApplication UseErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}
