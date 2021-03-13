namespace MyLotrApi.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using MyLotrApi.Exceptions;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (TheOneApiException ex)
            {
                await HandleExceptionAsync(context, ex.StatusCode, ex.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unhandled error occured");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode httpCode, string? message = null)
        {
            context.Response.StatusCode = (int)httpCode;
            return context.Response.WriteAsync(message ?? string.Empty);
        }
    }
}