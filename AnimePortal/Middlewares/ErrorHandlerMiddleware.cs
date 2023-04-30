﻿using AnimePortalAuthServer.Errors;
using System.Net;
using Core.Exceptions;

namespace AnimePortalAuthServer.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IHostEnvironment env)
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
                (HttpStatusCode statusCode, string message) = ex switch
                {
                    UnauthorizedAccessException => (HttpStatusCode.Forbidden, "You are not authorized"),
                    ApplicationException => (HttpStatusCode.BadRequest, "Bad request"),
                    NotFoundException => (HttpStatusCode.NotFound, ex.Message),
                    ArgumentNullException => (HttpStatusCode.BadRequest,ex.Message),
                    _ => (HttpStatusCode.InternalServerError, ex.Message),
                };

                ApiError response = _env.IsDevelopment()
                    ? new ApiError(
                        (int)statusCode, message, ex.StackTrace?.ToString())
                    : new ApiError(
                        (int)statusCode, message);

                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
