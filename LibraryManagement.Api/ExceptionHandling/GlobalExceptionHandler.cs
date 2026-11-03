using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace LibraryManagement.Api.ExceptionHandling;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An error occurred.");

        httpContext.Response.ContentType = "application/json";

        int statusCode;
        string message;

        // Duplicate data
        if (exception is InvalidOperationException)
        {
            statusCode = StatusCodes.Status409Conflict;
            message = exception.Message;
        }

        // Data not found
        else if (exception is KeyNotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            message = exception.Message;
        }

        // Any unexpected error
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            message = "An unexpected error occurred.";
        }

        httpContext.Response.StatusCode = statusCode;

        var response = new
        {
            success = false,
            message = message,
            data = (object?)null
        };

        await httpContext.Response.WriteAsync(
            JsonSerializer.Serialize(response),
            cancellationToken);

        return true;
    }
}