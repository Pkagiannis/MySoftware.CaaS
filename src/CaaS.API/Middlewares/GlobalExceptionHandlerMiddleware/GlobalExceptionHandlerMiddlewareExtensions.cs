namespace CaaS.API.Middlewares.GlobalExceptionHandlerMiddleware;

public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder) =>
        builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}