using System.Text.Json;
using IncomeTaxCalculator.Application.Exceptions;

namespace IncomeTaxCalculator.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = exception switch
        {
            NegativeSalaryException => StatusCodes.Status400BadRequest,

            _ => StatusCodes.Status500InternalServerError,
        };

        context.Response.ContentType = "application/json";

        var innerExceptionMes = string.Empty;
        if (exception.InnerException?.Message is not null)
        {
            innerExceptionMes = exception.InnerException?.Message;
        }

        var errorObject = new { error = exception.Message + innerExceptionMes };
        var errorJson = JsonSerializer.Serialize(errorObject);
        await context.Response.WriteAsync(errorJson);
    }
}