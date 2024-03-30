using IncomeTaxCalculator.Application.Exceptions;
using IncomeTaxCalculator.Middleware;
using Microsoft.AspNetCore.Http;

namespace IncomeTaxCalculator.Presentation.Tests.MiddlewareTests;

public class ExceptionHandlerMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_NoExceptionOccurs_NextDelegateIsInvoked()
    {
        // Arrange
        var middleware = new ExceptionHandlerMiddleware(_ => Task.CompletedTask);
        var context = new DefaultHttpContext();

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_NegativeSalaryExceptionOccurs_ReturnsBadRequest()
    {
        // Arrange
        var middleware = new ExceptionHandlerMiddleware(_ => 
            throw new NegativeSalaryException());
        var context = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();

        Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
        Assert.NotNull(responseBody);
        Assert.NotEmpty(responseBody);
    }
    
    [Fact]
    public async Task InvokeAsync_ExceptionOccursWithInnerException_ReturnsBadRequest()
    {
        // Arrange
        const string exceptionMsg = "Exception occured.";
        const string innerExceptionMsg = "Some Inner Exception.";
        var middleware = new ExceptionHandlerMiddleware(_ =>
            throw new NegativeSalaryException(exceptionMsg, new Exception(innerExceptionMsg)));
        var context = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();

        Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
        Assert.Contains(exceptionMsg + innerExceptionMsg, responseBody);
    }

    [Fact]
    public async Task InvokeAsync_GenericExceptionOccurs_ReturnsInternalServerError()
    {
        // Arrange
        var middleware = new ExceptionHandlerMiddleware(_ =>
            throw new Exception("An error occurred."));
        var context = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();

        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        Assert.Contains("An error occurred.", responseBody);
    }
}
