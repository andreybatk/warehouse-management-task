using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagement.API.Middlewares;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred while processing your request",
            Status = StatusCodes.Status500InternalServerError,
            Type = exception.GetType().Name,
            Detail = exception.Message
        };

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case Domain.Exceptions.ValidationException validationException:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Validation error";
                problemDetails.Type = "Validation failure";
                problemDetails.Detail = "One or more validation errors occurred";
                problemDetails.Extensions["errors"] = validationException.Errors;
                break;

            case Domain.Exceptions.NotFoundException notFoundException:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Entity not found";
                problemDetails.Detail = notFoundException.Message;
                break;

            case Domain.Exceptions.BadRequestException badRequestException:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Bad request";
                problemDetails.Detail = badRequestException.Message;
                break;
        }

        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
