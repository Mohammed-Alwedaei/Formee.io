using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Middlewares;

/// <summary>
/// Global Exception Handler class manages the exceptions thrown in the domain layer
/// </summary>
public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Catch the exceptions (If Any) and respond with the correspond status code
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
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
