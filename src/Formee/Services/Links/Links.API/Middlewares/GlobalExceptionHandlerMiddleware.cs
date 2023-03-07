using Links.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Links.API.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            int status;
            string title;

            if (exception.GetType() == typeof(NotFoundException))
            {
                status = StatusCodes.Status404NotFound;
                title = "Not Found";
            }
            else if (exception.GetType() == typeof(UnauthorizedException))
            {
                status = StatusCodes.Status401Unauthorized;
                title = "Unauthorized Request";
            }
            else if (exception.GetType() == typeof(BadRequestException))
            {
                status = StatusCodes.Status400BadRequest;
                title = "Bad Request";
            }
            else
            {
                status = StatusCodes.Status500InternalServerError;
                title = "Internal Server Error";
            }

            var problem = new ProblemDetails
            {
                Title = title,
                Status = status,
                Type = "Server Error",
                Detail = exception.Message
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = status;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}