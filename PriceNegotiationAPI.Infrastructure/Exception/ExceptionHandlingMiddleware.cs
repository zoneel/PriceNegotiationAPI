using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PriceNegotiationAPI.Domain.Exceptions;

namespace PriceNegotiationAPI.Infrastructure.Exception;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (System.Exception e)
        {
            string errorMessage = e switch
            {
                IdempotencyException idempotencyException => $"[{idempotencyException.statusCode}] {idempotencyException.GetType().Name} - {idempotencyException.Message}",
                ProductNotFoundException productNotFoundException => $"[{productNotFoundException.statusCode}] {productNotFoundException.GetType().Name} - {productNotFoundException.Message}",
                _ => "[Middleware] An unexpected error occurred."
            };

            await context.Response.WriteAsync(errorMessage);
        }
    }
}