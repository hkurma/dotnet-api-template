using System.Net;
using System.Text.Json;

namespace DotNet.Template.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            message = "An error occurred while processing your request.",
            details = exception.Message
        };

        context.Response.StatusCode = exception switch
        {
            ArgumentException or ArgumentNullException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        response = exception switch
        {
            ArgumentException or ArgumentNullException => new { message = "Invalid request parameters.", details = exception.Message },
            UnauthorizedAccessException => new { message = "Unauthorized access.", details = exception.Message },
            KeyNotFoundException => new { message = "Resource not found.", details = exception.Message },
            _ => new { message = "An internal server error occurred.", details = "Please try again later." }
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}
