using AnimePortalAuthServer.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace AnimePortalAuthServer.Extension
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

    }
}
