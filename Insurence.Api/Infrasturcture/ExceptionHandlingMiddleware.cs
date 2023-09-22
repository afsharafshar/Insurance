using System.Text.Json;

namespace Insurence.Api.Infrasturcture;

public static class ApplicationExtensionMethod
{

    public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}


public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger,RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (LogicException e)
        {
            context.Response.StatusCode = 200;
            await GenerateResponse(e.Message);
        }
        catch (Exception e)
        {
            var logError = $"Message={e.Message} \n stack Trace={e.StackTrace}";
            _logger.LogError(e,logError);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await GenerateResponse(e.Message);
        }
        
        async Task GenerateResponse(string message)
        {
            context.Response.ContentType = "application/json";
            var result = new { Message = message };
            var json = JsonSerializer.Serialize(result);
            await context.Response.WriteAsync(json);
        }
    }
}