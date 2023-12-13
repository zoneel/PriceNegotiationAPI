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
                string errorMessage = "[Middleware] An unexpected error occurred.";
                int statusCode = StatusCodes.Status500InternalServerError;

                switch (e)
                {
                    case IdempotencyException idempotencyException:
                        errorMessage = $"[{idempotencyException.StatusCode}] {idempotencyException.GetType().Name} - {idempotencyException.Message}";
                        statusCode = idempotencyException.StatusCode;
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        break;
                    case ProductNotFoundException productNotFoundException:
                        errorMessage = $"[{productNotFoundException.StatusCode}] {productNotFoundException.GetType().Name} - {productNotFoundException.Message}";
                        statusCode = productNotFoundException.StatusCode;
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    case InvalidCredentialsException invalidCredentialsException:
                        errorMessage = $"[{invalidCredentialsException.StatusCode}] {invalidCredentialsException.GetType().Name} - {invalidCredentialsException.Message}";
                        statusCode = invalidCredentialsException.StatusCode;
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    case HighballOfferException highballOfferException:
                        errorMessage = $"[{highballOfferException.StatusCode}] {highballOfferException.GetType().Name} - {highballOfferException.Message}";
                        statusCode = highballOfferException.StatusCode;
                        context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                        break;
                }

                _logger.LogError(errorMessage); // Log the error

                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(errorMessage);
            }
        }
    }