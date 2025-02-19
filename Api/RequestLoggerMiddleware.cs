namespace Api;

public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        await _next(context);
    }
}

public static class RequestLoggerMiddlewareExtensions
{
    public static void UseRequestLogger(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<RequestLoggerMiddleware>();
    }
}