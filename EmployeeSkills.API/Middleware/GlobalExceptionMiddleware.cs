using EmployeeSkills.Application.Common.Responses;
using System.Net;
using System.Text.Json;

namespace EmployeeSkills.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
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
            _logger.LogError(
                ex,
                "Unhandled exception for {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "An unexpected error occurred.",
            ErrorCode = "SERVER_ERROR",
            TraceId = context.TraceIdentifier
        };

        switch (exception)
        {
            case KeyNotFoundException:
                response = response with
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = exception.Message,
                    ErrorCode = "NOT_FOUND"
                };
                break;

            case UnauthorizedAccessException:
                response = response with
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = exception.Message,
                    ErrorCode = "UNAUTHORIZED"
                };
                break;

            case InvalidOperationException:
                response = response with
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = exception.Message,
                    ErrorCode = "INVALID_OPERATION"
                };
                break;
        }

        context.Response.StatusCode = response.StatusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}