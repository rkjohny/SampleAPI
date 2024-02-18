
namespace SampleAPI.Middlewares;

public class RequestEntryPointHandler(RequestDelegate next, ILogger<RequestEntryPointHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        logger.LogInformation("Adding new request: [{Method}{Path}{QueryString}]", context.Request.Method, context.Request.Path, (context.Request.QueryString.HasValue ? "/" + context.Request.QueryString.Value : ""));
        await next(context);
    }
}